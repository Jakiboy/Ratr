# Ratr Release Build Script
# This script builds a release version of Ratr with all dependencies

Write-Host "Building Ratr Release..." -ForegroundColor Green

# Set paths
$ProjectPath = "src\Ratr\Ratr.csproj"
$OutputPath = "release"
$DecoderSourcePath = "src\Ratr\bin\Debug\net6.0-windows\decoder"

# Clean previous release
if (Test-Path $OutputPath) {
    Write-Host "Cleaning previous release..." -ForegroundColor Yellow
    Remove-Item -Path $OutputPath -Recurse -Force
}

# Create release directory
New-Item -Path $OutputPath -ItemType Directory -Force | Out-Null

# Build the project in Release configuration
Write-Host "Building project in Release configuration..." -ForegroundColor Yellow
dotnet build $ProjectPath --configuration Release --verbosity minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

# Publish the application (self-contained)
Write-Host "Publishing self-contained application..." -ForegroundColor Yellow
dotnet publish $ProjectPath --configuration Release --output "$OutputPath\Ratr" --self-contained true --runtime win-x64 --verbosity minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "Publish failed!" -ForegroundColor Red
    exit 1
}

# Copy decoder executables
Write-Host "Copying decoder executables..." -ForegroundColor Yellow
if (Test-Path $DecoderSourcePath) {
    $DecoderDestPath = "$OutputPath\Ratr\decoder"
    New-Item -Path $DecoderDestPath -ItemType Directory -Force | Out-Null
    Copy-Item -Path "$DecoderSourcePath\*" -Destination $DecoderDestPath -Force
    Write-Host "Decoder executables copied successfully." -ForegroundColor Green
} else {
    Write-Host "Warning: Decoder executables not found at $DecoderSourcePath" -ForegroundColor Yellow
    Write-Host "Make sure to build the project in Debug mode first to generate decoder executables." -ForegroundColor Yellow
}

# Create a portable version (framework-dependent)
Write-Host "Creating portable version..." -ForegroundColor Yellow
dotnet publish $ProjectPath --configuration Release --output "$OutputPath\Ratr-Portable" --self-contained false --runtime win-x64 --verbosity minimal

# Copy decoder executables to portable version
if (Test-Path $DecoderSourcePath) {
    $DecoderDestPathPortable = "$OutputPath\Ratr-Portable\decoder"
    New-Item -Path $DecoderDestPathPortable -ItemType Directory -Force | Out-Null
    Copy-Item -Path "$DecoderSourcePath\*" -Destination $DecoderDestPathPortable -Force
}

# Create README for release
$ReadmeContent = @"
# Ratr v0.3.0 Release

## Contents

### Ratr (Self-contained)
- Complete standalone version that doesn't require .NET runtime
- Larger file size but runs on any Windows system
- Located in: Ratr\

### Ratr-Portable (Framework-dependent)
- Requires .NET 6.0 Desktop Runtime to be installed
- Smaller file size
- Located in: Ratr-Portable\

## Installation

### Self-contained version:
1. Extract the Ratr folder
2. Run Ratr.exe

### Portable version:
1. Install .NET 6.0 Desktop Runtime from: https://dotnet.microsoft.com/download/dotnet/6.0
2. Extract the Ratr-Portable folder
3. Run Ratr.exe

## Usage
1. Select your router model from the dropdown
2. Click "Choose file" to select your router configuration file
3. The application will decode and display the PPP credentials

## Supported Models
- Huawei DG8245V-10
- ZTE ZXHN F600W
- ZTE ZXHN H108N-2.5
- ZTE ZXHN H168N-3.1
- ZTE ZXHN H168N-3.5
- ZTE ZXHN H188A
- ZTE ZXHN H267A
- ZTE ZXHN H267N
- ZTE ZXHN H268N
- ZTE ZXHN H268Q
- ZTE ZXHN H288A
- ZTE ZXHN H298A
- ZTE ZXHN H298N
- ZTE ZXHN H298Q
- ZTE ZXV10 H201L-2.0

For more information, visit: https://github.com/Jakiboy/Ratr
"@

Set-Content -Path "$OutputPath\README.txt" -Value $ReadmeContent -Encoding UTF8

# Create version info
$VersionInfo = @"
Ratr v0.3.0
Build Date: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")
Target Framework: .NET 6.0 Windows
Author: Jakiboy
Repository: https://github.com/Jakiboy/Ratr
"@

Set-Content -Path "$OutputPath\VERSION.txt" -Value $VersionInfo -Encoding UTF8

Write-Host ""
Write-Host "Release build completed successfully!" -ForegroundColor Green
Write-Host "Output location: $OutputPath" -ForegroundColor Cyan
Write-Host ""
Write-Host "Contents:" -ForegroundColor Yellow
Write-Host "- Ratr\ (Self-contained version)" -ForegroundColor White
Write-Host "- Ratr-Portable\ (Framework-dependent version)" -ForegroundColor White
Write-Host "- README.txt (Installation and usage instructions)" -ForegroundColor White
Write-Host "- VERSION.txt (Build information)" -ForegroundColor White
