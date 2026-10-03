#!/usr/bin/env pwsh
<#
.SYNOPSIS
Prints the perf-gate prefixes affected by the changes since a base ref, as a JSON array, e.g. ["L02","S-DB01-0"].
Used by .github/workflows/perf-gate.yml to run only the affected gates.

Everything is discovered from the repo, nothing is listed by hand:
  - A lab's prefix is its folder name up to the first dash:  labs/L02-allocations -> L02.
  - A specialization is one gate: its prefix is the track prefix of its exercise names,
    S-DB01-01-order-feed and S-DB01-boss-sales-dashboard -> S-DB01.
  - A change under src/ affects every exercise or solution whose project references the changed project,
    directly or through another src project (the csproj ProjectReference graph).
  - Markdown files never affect a gate. Shared build files (see $shared) affect all of them.

.PARAMETER Base
Git ref to diff against (diffs Base...HEAD), e.g. origin/main.

.PARAMETER Stdin
Read the changed file names from stdin instead of running git diff. For testing the mapping.

.EXAMPLE
./scripts/changed-prefixes.ps1 origin/main
"labs/L02-allocations/exercises/L02-01-x/Workload.cs" | ./scripts/changed-prefixes.ps1 -Stdin
#>
param(
    [string]$Base,
    [switch]$Stdin
)

Set-Location (Join-Path $PSScriptRoot "..")

# Files that can move any budget in any lab.
$shared = '^(Directory\.Build\.props|Directory\.Packages\.props|global\.json|nuget\.config|scripts/perf-gate\.ps1|scripts/changed-prefixes\.ps1|\.github/workflows/perf-gate\.yml)$'

function Get-Prefix {
    # Maps a path to the gate prefix that runs it, or $null if the path is not inside an exercise/solution/lab.
    param([string]$Path)
    if ($Path -match '^labs/([^/]+)/') { return ($Matches[1] -split '-')[0] }
    if ($Path -match '^specializations/[^/]+/(?:exercises|solutions)/([^/]+)/') {
        $id = $Matches[1]
        if ($id -match '^(?<track>.+?)-(\d|boss)') { return $Matches.track }
        return $id
    }
    return $null
}

# Every gate prefix that exists on disk.
$allPrefixes = Get-ChildItem -Path "labs/*/exercises/*", "labs/*/solutions/*", "specializations/*/exercises/*", "specializations/*/solutions/*" -Directory -ErrorAction SilentlyContinue |
    ForEach-Object { Get-Prefix ((Resolve-Path -Relative $_.FullName) -replace '^\.[\\/]', '' -replace '\\', '/' | ForEach-Object { "$_/" }) } |
    Where-Object { $_ } | Sort-Object -Unique

# The project reference graph: project name -> names of the projects it references.
$projects = @{}
Get-ChildItem -Path src, labs, specializations -Recurse -Filter *.csproj -ErrorAction SilentlyContinue |
    Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' } | ForEach-Object {
        $rel = (Resolve-Path -Relative $_.FullName) -replace '^\.[\\/]', '' -replace '\\', '/'
        $refs = @(([xml](Get-Content $_.FullName -Raw)).Project.ItemGroup.ProjectReference.Include | Where-Object { $_ } |
            ForEach-Object { [IO.Path]::GetFileNameWithoutExtension(($_ -replace '\\', '/')) })
        $projects[$rel] = [pscustomobject]@{ Name = $_.BaseName; Refs = $refs }
    }

if ($Stdin) {
    # From a pipeline (`"a" | ./changed-prefixes.ps1 -Stdin`) or from the OS stdin (`pwsh -File ... -Stdin < list.txt`).
    $files = if ($MyInvocation.ExpectingInput) { @($input) } else { [Console]::In.ReadToEnd() -split "\r?\n" }
} else {
    if (-not $Base) { throw "usage: changed-prefixes.ps1 <base-ref> | -Stdin" }
    $files = & git diff --name-only "$Base...HEAD"
    if ($LASTEXITCODE -ne 0) { throw "git diff failed" }
}
$files = @($files | Where-Object { $_ } | ForEach-Object { $_ -replace '\\', '/' } | Where-Object { $_ -notmatch '\.md$' })

$hit = [System.Collections.Generic.HashSet[string]]::new()
$selectAll = $false
$changedLibs = [System.Collections.Generic.HashSet[string]]::new()

foreach ($f in $files) {
    if ($f -match $shared) { $selectAll = $true; continue }
    if ($f -match '^src/([^/]+)/') {
        $dir = $Matches[1]
        # A src project is named after its folder. Anything else directly under src/ is shared.
        if ($projects.Values | Where-Object { $_.Name -eq $dir }) { [void]$changedLibs.Add($dir) } else { $selectAll = $true }
        continue
    }
    $p = Get-Prefix $f
    if ($p) { [void]$hit.Add($p) }
}

# Follow ProjectReferences outwards from each changed src project until nothing new is affected.
$affected = [System.Collections.Generic.HashSet[string]]::new($changedLibs)
do {
    $grew = $false
    foreach ($proj in $projects.Values) {
        if (-not $affected.Contains($proj.Name) -and ($proj.Refs | Where-Object { $affected.Contains($_) })) {
            [void]$affected.Add($proj.Name); $grew = $true
        }
    }
} while ($grew)
foreach ($entry in $projects.GetEnumerator()) {
    if ($entry.Key -like 'src/*') { continue }
    if ($affected.Contains($entry.Value.Name)) { $p = Get-Prefix $entry.Key; if ($p) { [void]$hit.Add($p) } }
}

$selected = if ($selectAll) { $allPrefixes } else { @($allPrefixes | Where-Object { $hit.Contains($_) }) }
ConvertTo-Json -InputObject @($selected) -Compress
