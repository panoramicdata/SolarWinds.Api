# Panoramic Data NuGet Publish Script (Standard)
# Tags the current commit with the NBGV version and pushes to trigger CI/CD publishing.
# Usage: .\Publish.ps1

$ErrorActionPreference = 'Stop'

# Check for clean working tree before regeneration.
$status = git status --porcelain
if ($status) {
    Write-Error "Working tree is not clean. Commit or stash changes before publishing.`n$status"
    exit 1
}

# Ensure we are on the main branch.
$branch = git rev-parse --abbrev-ref HEAD
if ($branch -ne 'main') {
    Write-Error "Publishing is only supported from the 'main' branch (currently on '$branch')."
    exit 1
}

# Regenerate and validate the checked-in Service Desk OpenAPI document.
$openApiProject = Join-Path $PSScriptRoot 'SolarWinds.Api.OpenApi\SolarWinds.Api.OpenApi.csproj'
$openApiOutput = Join-Path $PSScriptRoot 'SolarWinds.ServiceDesk.OpenApi.json'

Write-Host "Generating Service Desk OpenAPI document ..." -ForegroundColor Cyan
dotnet run --project $openApiProject --configuration Release -- $openApiOutput | Out-Host

$openApiDiff = git status --porcelain -- SolarWinds.ServiceDesk.OpenApi.json
if ($openApiDiff) {
    Write-Host "Committing regenerated Service Desk OpenAPI document ..." -ForegroundColor Cyan
    git add -- SolarWinds.ServiceDesk.OpenApi.json
    git commit -m "Update generated ServiceDesk OpenAPI document"
}

# Ensure local main is up to date with remote (or ahead due auto-commit).
git fetch origin main --quiet
$localHead = git rev-parse HEAD
$remoteHead = git rev-parse origin/main
& git merge-base --is-ancestor origin/main HEAD
if ($LASTEXITCODE -ne 0) {
    Write-Error "Local branch is behind or diverged from origin/main. Pull/rebase and retry."
    exit 1
}

if ($localHead -ne $remoteHead) {
    Write-Host "Pushing local main updates before tagging ..." -ForegroundColor Cyan
    git push origin main
}

# Get version from NBGV
$versionJson = nbgv get-version -f json | ConvertFrom-Json
$version = $versionJson.SimpleVersion

if (-not $version) {
    Write-Error "Failed to determine version from nbgv."
    exit 1
}

# Check tag doesn't already exist
$existingTag = git tag -l $version
if ($existingTag) {
    Write-Error "Tag '$version' already exists."
    exit 1
}

Write-Host "Tagging as $version ..." -ForegroundColor Cyan
git tag $version
git push origin $version

Write-Host "Published tag $version. CI will build and push to NuGet." -ForegroundColor Green
