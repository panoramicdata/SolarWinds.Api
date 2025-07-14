param([string]$Configuration = "Release")

Write-Host "Reading NuGet API key..."
try {
    $nugetKey = Get-Content -Path "nuget.key" -Raw
}
catch {
    Write-Error "Failed to read nuget.key. Please create this file with your NuGet API key.";
    exit 1
}

Write-Host "Packing NuGet package..."
dotnet pack SolarWinds.Api.csproj -c $Configuration --no-build --output "."

Write-Host "Pushing NuGet package..."
Get-ChildItem *.nupkg | ForEach-Object {
    dotnet nuget push $_.FullName -s https://api.nuget.org/v3/index.json -k $nugetKey --skip-duplicate
}
