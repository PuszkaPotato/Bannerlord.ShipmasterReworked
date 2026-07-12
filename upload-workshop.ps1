$ErrorActionPreference = "Stop"

Write-Host "=== Bannerlord Workshop Upload ==="

# --- Configuration -----------------------------------------------------------
$moduleId    = "Bannerlord.ShipmasterReworked"
$projectPath = Join-Path $PSScriptRoot "Bannerlord.ShipmasterReworked\Bannerlord.ShipmasterReworked.csproj"
$stagingDir  = Join-Path $PSScriptRoot ".workshop-build"

# WorkshopUpdate.xml lives next to this script
$xmlFile = Join-Path $PSScriptRoot "WorkshopUpdate.xml"
if (-not (Test-Path $xmlFile)) {
    Write-Error "WorkshopUpdate.xml not found next to the script ($xmlFile)."
}
Write-Host "Found WorkshopUpdate.xml"

# --- Locate Steam / game install (used to find the Workshop uploader exe) -----
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

# --- Clean staged build ------------------------------------------------------
# Upload exactly what the current source produces: wipe the staging folder,
# build fresh, and upload from there. This avoids shipping stale or orphaned
# artifacts (e.g. an old versioned DLL) and never touches the live game install.

if (-not ($env:BANNERLORD_GAME_DIR -and (Test-Path $env:BANNERLORD_GAME_DIR))) {
    Write-Host "Setting BANNERLORD_GAME_DIR for the build: $gameRoot"
    $env:BANNERLORD_GAME_DIR = $gameRoot
}

$dotnetCmd = Get-Command dotnet -ErrorAction SilentlyContinue
if (-not $dotnetCmd) {
    Write-Error "'dotnet' was not found on PATH. Install the .NET SDK or add it to PATH."
}
$dotnet = $dotnetCmd.Source

if (-not (Test-Path $projectPath)) {
    Write-Error "Project file not found: $projectPath"
}

Write-Host "Cleaning staging folder: $stagingDir"
if (Test-Path $stagingDir) {
    Remove-Item $stagingDir -Recurse -Force
}
# GameFolder must exist BEFORE the build, or the module-copy targets are skipped.
New-Item -ItemType Directory -Path $stagingDir -Force | Out-Null

Write-Host "Building module (Release) into staging folder..."
& $dotnet build $projectPath -c Release --nologo "-p:GameFolder=$stagingDir"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed with exit code $LASTEXITCODE. Aborting upload."
}

# The build deploys the module to <GameFolder>/Modules/<ModuleId>
$moduleFolder = Join-Path $stagingDir "Modules\$moduleId"
$deployedSubModule = Join-Path $moduleFolder "SubModule.xml"
if (-not (Test-Path $deployedSubModule)) {
    Write-Error "Build did not produce a module at: $moduleFolder (missing SubModule.xml). Aborting upload."
}
Write-Host "Clean build ready at: $moduleFolder"

# --- Upload ------------------------------------------------------------------
Write-Host "Uploading mod to Steam Workshop..."
# Inject the freshly built module path into the XML's ModuleFolder Value
$tempXmlFile = $null
try {
    $xml = [xml](Get-Content $xmlFile)
    $moduleNode = $xml.SelectSingleNode('//Tasks/UpdateItem/ModuleFolder')
    if ($null -eq $moduleNode) {
        Write-Error "No //Tasks/UpdateItem/ModuleFolder node found in WorkshopUpdate.xml."
    }
    $moduleNode.SetAttribute('Value', $moduleFolder)
    $tempXmlFile = Join-Path $PSScriptRoot "WorkshopUpdate.injected.xml"
    $xml.Save($tempXmlFile)
    Write-Host "Using temporary XML: $tempXmlFile (ModuleFolder -> $moduleFolder)"

    # Run upload
    & $workshopExe $tempXmlFile
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
