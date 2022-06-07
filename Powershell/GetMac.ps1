<#
    .Description 
        Script to read the Mac-Address of specifed computers using specified credentials.
        If the scriptparameters are empty, the computername and credentials of the current computer and user will be used.
    
    .Parameter ComputerName
        Computername where you want to know the Mac-Address (if this parameter is empty, the local computername will be used)
    .Parameter UserName 
        Username of the user, which will be used for the credentials (if this parameter is empty, the current user will be used)
    .Parameter Password 
        Password of the user, which will be used for the credentials (if the username and this parameter is empty, the current user will be used)
    .Parameter Silent 
        If this parameter will be used, there will be no User-Interaction.

#>

[CmdletBinding()]
param 
(
    [string] $ComputerName,
    [string] $UserName,
    [string] $Password,
    [switch] $Silent
)

<#
.DESCRIPTION
    Reads out the MAC address of the desired computer using the desired credentials.
.PARAMETER computer
    Name of the desired computer/NULL
.PARAMETER credentials
    Credentials of the desired user/NULL
.NOTES
    return -> loaded MAC-Address/NULL/ERROR
#>
Function GetMacAddress
{   
    param ( $computer , $credential )
    #to make it work without parameters
    if([string ]::IsNullOrEmpty($computer)) 
    { 
        $computer = $env:COMPUTERNAME 
    
    }
    #program logic
    $hostIp = [System.Net.Dns]::GetHostByName($computer).AddressList[0].IpAddressToString
    if($credential) 
    {
        $credential = Get-Credential $credential
        $wmi = Get-WmiObject -Class Win32_NetworkAdapterConfiguration -Credential $credential -ComputerName $computer
    } else 
    {
        $wmi = Get-WmiObject -Class Win32_NetworkAdapterConfiguration -ComputerName $computer
    }
    return ($wmi | Where-Object { $_.IpAddress -eq $hostIp }).MACAddress
}

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

Write-Host "`nValidate script-parameters..."
if (!$Silent) 
{
    if ([string]::IsNullOrEmpty($ComputerName)) 
    {
        $ComputerName = Read-Host "`nPlease enter computername (for local computer leaf it empty and press enter)"
    }
    if ([string]::IsNullOrEmpty($UserName)) 
    {
        $UserName = Read-Host "`nPlease enter username (for the local user leaf it empty and press enter)"
    }

    [securestring] $securePwd = $null
    if ([string]::IsNullOrEmpty($Password)) 
    {
        if ( ![string]::IsNullOrEmpty($UserName)) 
        {
            $securePwd = Read-Host -assecurestring "`nPlease enter password"
        }
    }
    else 
    {
        $securePwd = ConvertTo-SecureString $Password –asplaintext –force 
    }
}

# Create Credential-Object
[pscredential] $credObject = $null
if (![string]::IsNullOrEmpty($UserName) -and $null -ne $securePwd)
{
    Write-Host "`nCreate Credential-Object..."
    [pscredential] $credObject = New-Object System.Management.Automation.PSCredential ($UserName, $securePwd)
}


# Get Mac-Address
$macAddress = GetMacAddress -computer $ComputerName -credential $credObject

if (![string]::IsNullOrEmpty($macAddress))
{
    Write-Host "`nMac-Address: `"$($macAddress)`"`n" -ForegroundColor Green
}
else 
{
    Write-Error "Something went wrong!`nCould not find Mac-Address."
}

if (!$Silent) 
{
    Write-Host -NoNewLine '`n`nPress any key to continue...';
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
}