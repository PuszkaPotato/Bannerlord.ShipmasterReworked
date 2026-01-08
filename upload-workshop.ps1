$ErrorActionPreference = "Stop"

Write-Host "=== Bannerlord Workshop Upload ==="

# XML must exist in current directory
$xmlFile = Join-Path (Get-Location) "WorkshopUpdate.xml"
if (-not (Test-Path $xmlFile)) {
    Write-Error "WorkshopUpdate.xml not found in current directory."
}

Write-Host "Found WorkshopUpdate.xml"

# Get Steam install path from registry
$steamRoot = Get-ItemPropertyValue `
    -Path "HKCU:\Software\Valve\Steam" `
    -Name "SteamPath" `
    -ErrorAction SilentlyContinue

if (-not $steamRoot -or -not (Test-Path $steamRoot)) {
    Write-Error "Steam installation not found via registry."
}

Write-Host "Steam found at: $steamRoot"

# Bannerlord workshop tool path
$workshopExe = Join-Path $steamRoot `
    "steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.MountAndBlade.SteamWorkshop.exe"

if (-not (Test-Path $workshopExe)) {
    Write-Error "Bannerlord Workshop executable not found."
}

Write-Host "Workshop tool found."

# Run upload
Write-Host "Uploading mod to Steam Workshop..."
& $workshopExe $xmlFile

Write-Host "Upload finished."
