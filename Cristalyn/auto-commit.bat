@echo off
echo Starting auto commit process...

REM Change to the project directory
cd /d "C:\Users\Asus\source\repos\Cristalyn-TWEBProiectan\Cristalyn"

REM Add all changes
git add .

REM Check if there are changes
git diff --quiet && git diff --staged --quiet
if %errorlevel% equ 0 (
    echo No changes to commit.
    exit /b 0
)

REM Commit changes with timestamp
for /f "tokens=2 delims==" %%a in ('wmic OS Get localdatetime /value') do set "dt=%%a"
set "YY=%dt:~2,2%" & set "YYYY=%dt:~0,4%" & set "MM=%dt:~4,2%" & set "DD=%dt:~6,2%"
set "HH=%dt:~8,2%" & set "Min=%dt:~10,2%" & set "Sec=%dt:~12,2%"
set "datestamp=%YYYY%-%MM%-%DD% %HH%:%Min%:%Sec%"

git commit -m "Auto commit: %datestamp%"

REM Push to remote
git push

echo Auto commit completed successfully! 