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

.EXAMPLE
./scripts/perf-gate.ps1
./scripts/perf-gate.ps1 L02
#>
param(
    [string]$Prefix = "",
    [int]$Retries = 2
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
        & dotnet run -c Release --no-build --project $proj *> $null
        $LASTEXITCODE
    } -ArgumentList $Path

    if (Wait-Job $job -Timeout 300) {
        $rc = Receive-Job $job
    } else {
        Stop-Job $job
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
    $dirs = @("labs/*/$kind/$Prefix*", "specializations/*/$kind/$Prefix*") |
        ForEach-Object { Get-ChildItem -Path $_ -Directory -ErrorAction SilentlyContinue } |
        Sort-Object FullName

    foreach ($d in $dirs) {
        $programCs = Join-Path $d.FullName "Program.cs"
        $hasProgram = Test-Path $programCs
        if ($kind -eq "exercises" -and -not $hasProgram) { continue }

        $name = "$kind/$($d.Name)"

        $total++

        $attempt = 0
        do {
            $attempt++
            $rc = Invoke-Project $d.FullName
        } while ($rc -ne $want -and $rc -ne 2 -and $attempt -le $Retries)

        $verdict = "OK"
        if ($rc -ne $want) { $fail = 1; $verdict = "SURPRISE" }
        elseif ($attempt -gt 1) { $verdict = "OK (attempt $attempt)" }
        "{0,-34} {1,-9} {2,-9} {3}" -f $name, "exit $want", "exit $rc", $verdict | Write-Host
    }
}

$summary = if ($fail -eq 0) { "all as expected" } else { "SURPRISES found" }
Write-Host "checked $total projects: $summary"
exit $fail
