#!/usr/bin/env pwsh
<#
.SYNOPSIS
Perf gate: every *solution* must PASS its budgets (exit 0) and every *exercise* must FAIL them (exit 1).
A green run means the budgets still separate the fixed code from the slow code, i.e. the gate can actually
catch regressions.

.PARAMETER Prefix
Optional name prefix to restrict which exercises/solutions are checked, e.g. "L02" or "L04-01".

.PARAMETER Retries
How many extra attempts a project gets when its exit code is not the expected one. Time budgets are noisy on shared
CI runners; a real regression fails every attempt, a noisy-neighbour blip does not. Exit code 2 (wrong result) is
deterministic and is never retried.

.PARAMETER SolutionTimeSlack
Multiplier (>= 1) applied to the time-scaled budgets (time, p99, cpu) of *solutions* only, via PERFLAB_TIME_SLACK.
Concurrent workloads (an in-process web server driven by many virtual users) depend on core count, which the
single-core machine factor cannot capture, so a CI runner with few cores fails budgets calibrated on a big machine.
Exercises always run against the strict budgets, and allocation/counter budgets are never scaled.

.EXAMPLE
./scripts/perf-gate.ps1
./scripts/perf-gate.ps1 L02
#>
param(
    [string]$Prefix = "",
    [int]$Retries = 2,
    [double]$SolutionTimeSlack = 1.0
)

Set-Location (Join-Path $PSScriptRoot "..")

function Invoke-Build {
    param([string]$SlnPath)
    $output = & dotnet build $SlnPath -c Release -v q 2>&1
    $errorLines = $output | Select-String -CaseSensitive -Pattern "error|Build FAILED"
    if ($errorLines) {
        $errorLines | ForEach-Object { Write-Host $_.Line }
        Write-Host "build failed"
        exit 1
    }
}

function Invoke-Project {
    param([string]$Path)
    $job = Start-Job -ScriptBlock {
        param($proj)
        $text = & dotnet run -c Release --no-build --project $proj 2>&1 | Out-String
        [pscustomobject]@{ Rc = $LASTEXITCODE; Text = $text }
    } -ArgumentList $Path

    if (Wait-Job $job -Timeout 300) {
        $r = Receive-Job $job
        $script:LastOutput = $r.Text
        $rc = $r.Rc
    } else {
        Stop-Job $job
        $script:LastOutput = "timed out after 300 s"
        $rc = 124
    }
    Remove-Job $job -Force
    return $rc
}

Invoke-Build "PerfLab.slnx"
Invoke-Build "PerfLab.Solutions.slnx"

"{0,-34} {1,-9} {2,-9} {3}" -f "project", "expected", "actual", "verdict" | Write-Host

$fail = 0
$total = 0

foreach ($kind in @("solutions", "exercises")) {
    $want = if ($kind -eq "exercises") { 1 } else { 0 }
    if ($kind -eq "solutions" -and $SolutionTimeSlack -gt 1.0) { $env:PERFLAB_TIME_SLACK = "$SolutionTimeSlack" }
    else { Remove-Item Env:PERFLAB_TIME_SLACK -ErrorAction SilentlyContinue }
    $dirs = @("labs/*/$kind/$Prefix*", "specializations/*/$kind/$Prefix*") |
        ForEach-Object { Get-ChildItem -Path $_ -Directory -ErrorAction SilentlyContinue } |
        Sort-Object FullName

    foreach ($d in $dirs) {
        $programCs = Join-Path $d.FullName "Program.cs"
        $hasProgram = Test-Path $programCs
        if ($kind -eq "exercises" -and -not $hasProgram) { continue }

        $name = "$kind/$($d.Name)"

        # An exercise with no budgets at all is pure exploration (see LabSpec.MaxMetrics): it cannot fail, so the
        # gate expects it to pass. Every other exercise must fail its budgets.
        $wantHere = $want
        if ($kind -eq "exercises" -and (Get-Content $programCs -Raw) -notmatch 'MaxMetrics|MaxFirstRunMs') { $wantHere = 0 }

        $total++

        $attempt = 0
        do {
            $attempt++
            $rc = Invoke-Project $d.FullName
        } while ($rc -ne $wantHere -and $rc -ne 2 -and $attempt -le $Retries)

        $verdict = "OK"
        if ($rc -ne $wantHere) { $fail = 1; $verdict = "SURPRISE" }
        elseif ($attempt -gt 1) { $verdict = "OK (attempt $attempt)" }
        "{0,-34} {1,-9} {2,-9} {3}" -f $name, "exit $wantHere", "exit $rc", $verdict | Write-Host
        if ($rc -ne $wantHere) {
            # Show why: the machine factor and the metric table of the last attempt, so a CI log is enough to
            # tell a tight budget from noise.
            $lines = @($script:LastOutput -split "\r?\n" | Where-Object { $_.Trim() }) | Select-Object -Last 25
            $lines | ForEach-Object { Write-Host "    $_" }
        }
    }
}

Remove-Item Env:PERFLAB_TIME_SLACK -ErrorAction SilentlyContinue
$summary = if ($fail -eq 0) { "all as expected" } else { "SURPRISES found" }
Write-Host "checked $total projects: $summary"
exit $fail
