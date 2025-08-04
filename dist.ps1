# Create distribution ZIP files for Ratr release
param(
    [string]$Version = "0.4.0"
)

Write-Host "Creating distribution packages for Ratr v$Version..." -ForegroundColor Green

$ReleasePath = "release"
$DistPath = "dist"

# Create dist directory
if (Test-Path $DistPath) {
    Remove-Item -Path $DistPath -Recurse -Force
}
New-Item -Path $DistPath -ItemType Directory -Force | Out-Null

# Function to create ZIP file
function Create-ZipFile {
    param(
        [string]$SourcePath,
        [string]$DestinationPath
    )
    
    if (Get-Command "Compress-Archive" -ErrorAction SilentlyContinue) {
        Compress-Archive -Path $SourcePath -DestinationPath $DestinationPath -Force
        Write-Host "Created: $DestinationPath" -ForegroundColor Green
    } else {
        Write-Host "PowerShell Compress-Archive not available. Please manually zip the folders." -ForegroundColor Yellow
    }
}

# Create ZIP packages
if (Test-Path "$ReleasePath\Ratr") {
    $ZipName = "$DistPath\Ratr.zip"
    Create-ZipFile -SourcePath "$ReleasePath\Ratr\*" -DestinationPath $ZipName
}

if (Test-Path "$ReleasePath\Ratr-Standalone") {
    $ZipName = "$DistPath\Ratr-Standalone.zip"
    Create-ZipFile -SourcePath "$ReleasePath\Ratr-Standalone\*" -DestinationPath $ZipName
}

if (Test-Path "$ReleasePath\Ratr-Without-Decoders") {
    $ZipName = "$DistPath\Ratr-Without-Decoders.zip"
    Create-ZipFile -SourcePath "$ReleasePath\Ratr-Without-Decoders\*" -DestinationPath $ZipName
}

# Create checksums
if (Test-Path $DistPath) {
    $ChecksumFile = "$DistPath\checksums.txt"
    $Checksums = @()
    
    Get-ChildItem -Path $DistPath -Filter "*.zip" | ForEach-Object {
        $Hash = Get-FileHash -Path $_.FullName -Algorithm SHA256
        $Checksums += "$($Hash.Hash.ToLower())  $($_.Name)"
    }
    
    $Checksums | Out-File -FilePath $ChecksumFile -Encoding UTF8
    Write-Host "Created checksums file: $ChecksumFile" -ForegroundColor Green
}

Write-Host ""
Write-Host "Distribution packages created successfully!" -ForegroundColor Green
Write-Host "Location: $DistPath" -ForegroundColor Cyan
if (Test-Path $DistPath) {
    Get-ChildItem -Path $DistPath | ForEach-Object {
        $Size = if ($_.Length -gt 1MB) { "{0:N1} MB" -f ($_.Length / 1MB) } else { "{0:N0} KB" -f ($_.Length / 1KB) }
        Write-Host "- $($_.Name) ($Size)" -ForegroundColor White
    }
}
