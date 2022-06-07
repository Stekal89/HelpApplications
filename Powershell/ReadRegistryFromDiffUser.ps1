<#
    .Description
    Skript um den Registry-Key "HKEY_CURRENT_USER" eines eingeloggten Users zu lesen. 

    .Parameter
       - UserName -> Name des Users von dem der Key geladen werden soll
       - KeyPath -> Registry-Key des Users, der geladen werden soll
                    "Computer\HKEY_CURRENT_USER" darf dabei nicht angegeben werden,
                    nur der Pfad, der danach kommt!!!
#>

#param
#(
#    [string] $UserName = "jetorbituser",
#    [string] $KeyPath = "Software\objectPlace\objectFactory\Programs\ReportCenter\ARAAbrechnung"
#)

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

[string] $UserName = Read-Host "Username"
if([string]::IsNullOrEmpty($UserName))
{
    Write-Host "Kein Username angegeben!"
    Read-Host "`n`nContinue with the Enter key..."
    exit
}

[string] $KeyPath = Read-Host "Key-Pfad (ohne `"HKEY_CURRENT_USER`")"
if([string]::IsNullOrEmpty($KeyPath))
{
    Write-Host "Kein KeyPath angegeben!"
    Read-Host "`n`nContinue with the Enter key..."
    exit
}

Write-Host "`nGet SessionId of user..."
$User = New-Object System.Security.Principal.NTAccount($UserName)
$sid = $User.Translate([System.Security.Principal.SecurityIdentifier]).value

Write-Host "`nVerify if path HKU already exist..."
$existingPSDrive = Get-PSDrive "HKU" -ErrorAction SilentlyContinue
if($null -eq $existingPSDrive)
{
    Write-Host "Path HKU does not exist, create path..."
    New-PSDrive HKU Registry HKEY_USERS
}

Write-Host "`nLoad Registry-Key `"HKU:\${sid}\$($KeyPath)`"..."
$regKey = Get-Item (Join-Path "HKU:\${sid}" $KeyPath) -Force -ErrorAction SilentlyContinue

if($null -ne $regKey)
{
    if($null -ne $regKey.Property)
    {
        foreach($item in $regKey.Property)
        {
            Write-Host "$($item): `"$($regKey.GetValue($item))`""
        }
    }
}
else
{
    Write-Host "Registry-Key: `"HKU:\${sid}\$($KeyPath)`" konnte nicht gefunden werden."
}

Read-Host "`n`nContinue with the Enter key..."