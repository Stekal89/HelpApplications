<#
.Description
Script to get the passwords of users, which are stored in the "Credential Manager"
#>

# Self-elevate the script if required
if (-Not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] 'Administrator')) 
{
    if ([int](Get-CimInstance -Class Win32_OperatingSystem | Select-Object -ExpandProperty BuildNumber) -ge 6000) 
    {
        $CommandLine = "-File `"" + $MyInvocation.MyCommand.Path + "`" " + $MyInvocation.UnboundArguments
        Start-Process -FilePath PowerShell.exe -Verb Runas -ArgumentList $CommandLine
        Exit
    }
}

if ($null -eq (Get-Module -ListAvailable -Name CredentialManager)) {
    Install-Module CredentialManager -Verbose
} 

[void][Reflection.Assembly]::LoadWithPartialName('Microsoft.VisualBasic')
$msg   = 'Please enter Username:'
$UserName = [Microsoft.VisualBasic.Interaction]::InputBox($msg, "GetCredentialsOfCredentialManager.ps1")

#if([string]::IsNullOrEmpty($UserName))
#{
#    Write-Host "ERROR -> The entered User-Name is NULL or EMPTY!"
#    Read-Host "Continue with the Enter Key..."
#    exit
#}

$UserName = $UserName.Trim()

$creds = Get-StoredCredential -ErrorAction SilentlyContinue -WarningAction SilentlyContinue -AsCredentialObject

if($null -eq $creds)
{
    Write-Host "ERROR -> No Credentials found!"
    Read-Host "Continue with the Enter Key..."
    exit
}

[boolean] $bMinOneFound = $false

for($i = 0; $i -lt $creds.Count; $i++)
{
    if($creds[$i].UserName.Trim() -like "*$($UserName)*")
    {
        Write-Host "----------------------------------"
        $creds[$i]
        Write-Host "----------------------------------"
        $bMinOneFound = $true
    }
}

if(-Not $bMinOneFound)
{
    Write-Host "ERROR -> Credentials of user `"$($UserName)`" not found!"
}

Read-Host "Continue with the Enter Key..."