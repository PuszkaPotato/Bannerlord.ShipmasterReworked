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

$gameRoot = $null

if ($steamRoot -and (Test-Path $steamRoot)) {
    Write-Host "Steam found at: $steamRoot"
    $gameRoot = Join-Path $steamRoot "steamapps\common\Mount & Blade II Bannerlord"
} else {
    Write-Host "Steam installation not found via registry; checking BANNERLORD_GAME_DIR environment variable..."
    $envGameDir = $env:BANNERLORD_GAME_DIR
    if ($envGameDir -and (Test-Path $envGameDir)) {
        Write-Host "Using BANNERLORD_GAME_DIR: $envGameDir"
        $gameRoot = $envGameDir
    } else {
        Write-Error "Bannerlord installation not found via registry or BANNERLORD_GAME_DIR."
    }
}

# Bannerlord workshop tool path (use resolved game root)
if (-not $gameRoot) {
    Write-Error "Bannerlord game root is not set."
}

# Resolve workshop executable path from the current game root
$workshopExe = Join-Path $gameRoot `
    "bin\Win64_Shipping_Client\TaleWorlds.MountAndBlade.SteamWorkshop.exe"

# If the exe isn't present at the Steam-derived location, allow overriding with env var
if (-not (Test-Path $workshopExe)) {
    Write-Host "Workshop executable not found at: $workshopExe"
    Write-Host "Checking BANNERLORD_GAME_DIR environment variable as fallback..."
    $envGameDir = $env:BANNERLORD_GAME_DIR
    if ($envGameDir -and (Test-Path $envGameDir)) {
        Write-Host "Using BANNERLORD_GAME_DIR: $envGameDir"
        $gameRoot = $envGameDir
        $workshopExe = Join-Path $gameRoot `
            "bin\Win64_Shipping_Client\TaleWorlds.MountAndBlade.SteamWorkshop.exe"
    }
}

if (-not (Test-Path $workshopExe)) {
    Write-Error "Bannerlord Workshop executable not found at: $workshopExe"
}

Write-Host "Workshop tool found at: $workshopExe"

# Run upload
Write-Host "Uploading mod to Steam Workshop..."
# If an environment variable is provided for the game directory, inject it into the XML's ModuleFolder Value
$tempXmlFile = $null
$envModuleFolder = $env:BANNERLORD_GAME_DIR
if ($envModuleFolder -and (Test-Path $envModuleFolder)) {
    try {
        Write-Host "Injecting BANNERLORD_GAME_DIR into XML ModuleFolder: $envModuleFolder"
        # Append the module subfolder
        $modulePath = Join-Path $envModuleFolder "Modules\Bannerlord.ShipmasterReworked"
        if (-not (Test-Path $modulePath)) {
            Write-Host "Warning: module directory not found at $modulePath; injecting path anyway."
        }
        $xml = [xml](Get-Content $xmlFile)
        $moduleNode = $xml.SelectSingleNode('//Tasks/UpdateItem/ModuleFolder')
        if ($moduleNode -ne $null) {
            $moduleNode.SetAttribute('Value', $modulePath)
            $tempXmlFile = Join-Path (Get-Location) "WorkshopUpdate.injected.xml"
            $xml.Save($tempXmlFile)
            Write-Host "Using temporary XML: $tempXmlFile"
        } else {
            Write-Host "No ModuleFolder node found in XML; using original file."
        }
    } catch {
        Write-Host "Failed to inject environment variable into XML: $_"
    }
}

if ($tempXmlFile) {
    $xmlToUse = $tempXmlFile
} else {
    $xmlToUse = $xmlFile
}

try {
    & $workshopExe $xmlToUse
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Workshop tool exited with code $LASTEXITCODE."
    }
    Write-Host "Upload finished."
} finally {
    # Always clean up the temporary XML, even if the upload failed
    if ($tempXmlFile -and (Test-Path $tempXmlFile)) {
        Remove-Item $tempXmlFile -ErrorAction SilentlyContinue
    }
}
