#!/usr/bin/env pwsh
<#
.SYNOPSIS
Prints the perf-gate prefixes affected by the changes since a base ref, as a JSON array, e.g. ["L02","S-DB01-0"].
Used by .github/workflows/perf-gate.yml to run only the affected gates.

.PARAMETER Base
Git ref to diff against (diffs Base...HEAD), e.g. origin/main.

.PARAMETER Stdin
Read the changed file names from stdin instead of running git diff. For testing the mapping.

.EXAMPLE
./scripts/changed-prefixes.ps1 origin/main
"labs/L02-allocations/README.md" | ./scripts/changed-prefixes.ps1 -Stdin
#>
param(
    [string]$Base,
    [switch]$Stdin
)

$all   = @(0..14 | ForEach-Object { "L{0:D2}" -f $_ }) + @("S-DB01-0", "S-DB01-1", "S-DB01-b")
$raven = @("S-DB01-0", "S-DB01-1", "S-DB01-b")

if ($Stdin) {
    # From a pipeline (`"a" | ./changed-prefixes.ps1 -Stdin`) or from the OS stdin (`pwsh -File ... -Stdin < list.txt`).
    $files = if ($MyInvocation.ExpectingInput) { @($input) } else { [Console]::In.ReadToEnd() -split "\r?\n" }
} else {
    if (-not $Base) { throw "usage: changed-prefixes.ps1 <base-ref> | -Stdin" }
    $files = & git diff --name-only "$Base...HEAD"
    if ($LASTEXITCODE -ne 0) { throw "git diff failed" }
}

# Shared harness, build settings and the gate itself: any budget can move, so run everything.
$everything = '^(src/(?!PerfLab\.Harness\.Raven/)|Directory\.Build\.props$|global\.json$|scripts/perf-gate\.ps1$|scripts/changed-prefixes\.ps1$|\.github/workflows/perf-gate\.yml$)'

$hit = [System.Collections.Generic.HashSet[string]]::new()
$selectAll = $false

foreach ($f in $files | Where-Object { $_ }) {
    $f = $f -replace '\\', '/'
    if ($f -match $everything) { $selectAll = $true }
    elseif ($f -match '^(src/PerfLab\.Harness\.Raven/|specializations/ravendb/)') { $raven | ForEach-Object { [void]$hit.Add($_) } }
    elseif ($f -match '^labs/(L\d\d)-') { [void]$hit.Add($Matches[1]) }
}

$selected = if ($selectAll) { $all } else { @($all | Where-Object { $hit.Contains($_) }) }
ConvertTo-Json -InputObject @($selected) -Compress
