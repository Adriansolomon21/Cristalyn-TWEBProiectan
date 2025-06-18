# Automatic GitHub Commits Setup Guide

This guide shows you how to set up automatic commits and pushes to GitHub for your Cristalyn project.

## Prerequisites

1. **Git installed** on your computer
2. **GitHub account** and repository created
3. **GitHub CLI** (optional, for easier setup)

## Method 1: GitHub Actions (Recommended)

### Setup Steps:

1. **Create GitHub Repository:**
   - Go to GitHub.com and create a new repository
   - Name it `Cristalyn` or whatever you prefer

2. **Initialize Git and Connect to GitHub:**
   ```bash
   git init
   git add .
   git commit -m "Initial commit"
   git branch -M main
   git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
   git push -u origin main
   ```

3. **GitHub Actions Workflow:**
   - The `.github/workflows/auto-commit.yml` file is already created
   - This will automatically run daily at 2 AM UTC
   - You can also trigger it manually from GitHub Actions tab

## Method 2: PowerShell Script (Local)

### Usage:

1. **Run manually:**
   ```powershell
   .\auto-commit.ps1
   ```

2. **Run with custom message:**
   ```powershell
   .\auto-commit.ps1 -CommitMessage "Your custom message"
   ```

3. **Schedule with Windows Task Scheduler:**
   - Open Task Scheduler
   - Create Basic Task
   - Set trigger (daily, weekly, etc.)
   - Action: Start a program
   - Program: `powershell.exe`
   - Arguments: `-ExecutionPolicy Bypass -File "C:\Users\Asus\source\repos\Cristalyn-TWEBProiectan\Cristalyn\auto-commit.ps1"`

## Method 3: Batch File with Task Scheduler

### Setup:

1. **Edit the batch file:**
   - Open `auto-commit.bat`
   - Verify the path matches your project location

2. **Schedule with Task Scheduler:**
   - Open Task Scheduler
   - Create Basic Task
   - Name: "Cristalyn Auto Commit"
   - Trigger: Daily at your preferred time
   - Action: Start a program
   - Program: `C:\Users\Asus\source\repos\Cristalyn-TWEBProiectan\Cristalyn\auto-commit.bat`

## Method 4: Git Hooks (Advanced)

### Pre-commit Hook:

Create `.git/hooks/pre-commit`:
```bash
#!/bin/sh
# Auto-commit hook
git add .
git commit -m "Auto commit: $(date)"
```

### Post-commit Hook:

Create `.git/hooks/post-commit`:
```bash
#!/bin/sh
# Auto-push hook
git push origin main
```

## Troubleshooting

### Common Issues:

1. **Authentication Error:**
   - Use GitHub Personal Access Token
   - Or set up SSH keys

2. **Repository Not Found:**
   - Check remote URL: `git remote -v`
   - Update if needed: `git remote set-url origin NEW_URL`

3. **Permission Denied:**
   - Ensure you have write access to the repository
   - Check GitHub repository settings

### Setup GitHub Authentication:

1. **Personal Access Token:**
   - Go to GitHub Settings → Developer settings → Personal access tokens
   - Generate new token with repo permissions
   - Use token as password when pushing

2. **SSH Keys:**
   ```bash
   ssh-keygen -t ed25519 -C "your_email@example.com"
   # Add public key to GitHub SSH keys
   ```

## Recommended Setup

For your Cristalyn project, I recommend:

1. **Start with Method 1 (GitHub Actions)** - most reliable
2. **Use Method 2 (PowerShell)** for immediate commits during development
3. **Set up Method 3 (Task Scheduler)** as backup for daily commits

## Next Steps

1. Create your GitHub repository
2. Run the initial setup commands
3. Test the auto-commit scripts
4. Set up scheduled tasks if desired

Your project will now automatically sync with GitHub! 