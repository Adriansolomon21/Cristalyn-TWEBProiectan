# Auto Commit and Push Script
# Run this script to automatically commit and push changes to GitHub

param(
    [string]$CommitMessage = "Auto commit: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
)

Write-Host "Starting auto commit process..." -ForegroundColor Green

# Check if git is available
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Host "Git is not installed or not in PATH!" -ForegroundColor Red
    exit 1
}

# Check if we're in a git repository
if (-not (Test-Path ".git")) {
    Write-Host "Not in a git repository. Initializing..." -ForegroundColor Yellow
    git init
}

# Add all changes
Write-Host "Adding all changes..." -ForegroundColor Blue
git add .

# Check if there are changes to commit
$status = git status --porcelain
if ([string]::IsNullOrEmpty($status)) {
    Write-Host "No changes to commit." -ForegroundColor Yellow
    exit 0
}

# Commit changes
Write-Host "Committing changes with message: $CommitMessage" -ForegroundColor Blue
git commit -m $CommitMessage

# Push to remote repository
Write-Host "Pushing to remote repository..." -ForegroundColor Blue
try {
    git push
    Write-Host "Successfully pushed changes to GitHub!" -ForegroundColor Green
} catch {
    Write-Host "Error pushing to GitHub. You may need to set up the remote repository first." -ForegroundColor Red
    Write-Host "To set up remote: git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git" -ForegroundColor Yellow
}

Write-Host "Auto commit process completed!" -ForegroundColor Green 