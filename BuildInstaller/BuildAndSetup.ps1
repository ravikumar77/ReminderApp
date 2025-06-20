# Build and Prepare Reminder App
# This script builds the solution, prepares the release, and provides instructions for creating the installer

# Parameters
$solutionPath = "\\midp-oaf-116\FldrRedir_3$\N994992\Data\reminder app\ReminderApp.sln"
$projectPath = "\\midp-oaf-116\FldrRedir_3$\N994992\Data\reminder app\ReminderApp\ReminderApp.csproj"
$publishDir = "\\midp-oaf-116\FldrRedir_3$\N994992\Data\reminder app\ReminderApp\bin\Release\net9.0-windows\publish"

function Write-Header {
    param([string]$text)
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host " $text" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
}

# Start the process
Clear-Host
Write-Header "REMINDER APP BUILD & INSTALLER SETUP"

# Check for solution file
Write-Host "Checking for solution file..." -ForegroundColor Yellow
if (-not (Test-Path $solutionPath)) {
    Write-Host "Solution file not found at: $solutionPath" -ForegroundColor Red
    exit 1
} else {
    Write-Host "Solution file found." -ForegroundColor Green
}

# Build the solution in Debug mode first to verify everything compiles
Write-Header "BUILDING DEBUG VERSION"
Write-Host "Building solution in Debug mode..." -ForegroundColor Yellow
dotnet build $solutionPath -c Debug

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error building Debug version. Please fix the errors and try again." -ForegroundColor Red
    exit 1
} else {
    Write-Host "Debug build successful." -ForegroundColor Green
}

# Build the Release version
Write-Header "BUILDING RELEASE VERSION"
Write-Host "Building solution in Release mode..." -ForegroundColor Yellow
dotnet build $solutionPath -c Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error building Release version. Please fix the errors and try again." -ForegroundColor Red
    exit 1
} else {
    Write-Host "Release build successful." -ForegroundColor Green
}

# Create a self-contained publish
Write-Header "PUBLISHING APPLICATION"
Write-Host "Publishing application for distribution..." -ForegroundColor Yellow
dotnet publish $solutionPath -c Release -r win-x64 --self-contained false

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error publishing application. Please fix the errors and try again." -ForegroundColor Red
    exit 1
} else {
    Write-Host "Application published successfully." -ForegroundColor Green
}

# Verify the published files
if (-not (Test-Path "$publishDir\ReminderApp.exe")) {
    Write-Host "Warning: Published executable not found at expected location." -ForegroundColor Yellow
    Write-Host "Expected path: $publishDir\ReminderApp.exe" -ForegroundColor Yellow
} else {
    Write-Host "Verified: Published executable found at: $publishDir\ReminderApp.exe" -ForegroundColor Green
}

# Final instructions
Write-Header "NEXT STEPS"
Write-Host "To create the installer:"
Write-Host "1. Download and install InnoSetup from https://jrsoftware.org/isdl.php" -ForegroundColor Yellow
Write-Host "2. Open InnoSetup Compiler" -ForegroundColor Yellow
Write-Host "3. Open the ReminderAppInstaller.iss file" -ForegroundColor Yellow
Write-Host "4. Click on 'Build' > 'Compile'" -ForegroundColor Yellow
Write-Host "5. The installer will be created in the 'Output' folder" -ForegroundColor Yellow
Write-Host ""
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
