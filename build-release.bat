@echo off
echo Building Ratr Release...

REM Check if PowerShell is available
powershell -Command "Get-Host" >nul 2>&1
if %errorlevel% neq 0 (
    echo PowerShell is required to run this build script.
    echo Please run build-release.ps1 directly or install PowerShell.
    pause
    exit /b 1
)

REM Run the PowerShell build script
powershell -ExecutionPolicy Bypass -File "build-release.ps1"

if %errorlevel% neq 0 (
    echo Build failed!
    pause
    exit /b 1
)

echo.
echo Build completed! Check the 'release' folder for output.
pause
