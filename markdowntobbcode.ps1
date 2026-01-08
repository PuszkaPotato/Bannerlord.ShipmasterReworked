$ErrorActionPreference = "Stop"

Write-Host "=== Markdown to BBCode ==="

Write-Host "Installing tools..."

dotnet tool install -g Converter.MarkdownToBBCodeNM.Tool
dotnet tool install -g Converter.MarkdownToBBCodeSteam.Tool

Write-Host "Converting README.md to BBCode formats..."

markdown_to_bbcodenm -i "./README.md" -o "./README_BBCODE_NM.txt"
markdown_to_bbcodesteam -i "./README.md" -o "./README_BBCODE_Steam.txt"

Write-Host "=== Completed ==="
Read-Host -Prompt "Press Enter to exit"