@echo off
echo Creating Ratr distribution packages...
powershell.exe -ExecutionPolicy Bypass -File "dist.ps1" %*
pause
