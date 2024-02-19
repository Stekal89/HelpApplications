<#
    .Description
    Script to get the Processes, which are blocking a folder/file

    Without Parameters
    
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

[string] $path = Read-Host "Please insert the path which should be checked"

$path = $path.TrimStart('"').TrimEnd('"')

if ([String]::IsNullOrEmpty($path)) 
{
    Write-Error "No user input exist!"
    Read-Host "`n`nContinue with the enter key..."
    exit 1
}

# Check if Path exist
IF(!(Test-Path -Path $path)) 
{
    Write-Warning "Folder, or file does not exits!"
    Read-Host "`n`nContinue with the enter key..."
    exit 1
}
Else 
{
    try
    {
        # Create and initialize ProcessStartInfo - Object
        $processInfo                        = [System.Diagnostics.ProcessStartInfo]::new()
        $processInfo.FileName               = "$($PSScriptRoot)\Handle\handle64.exe"
        $processInfo.UseShellExecute        = $false
        $processInfo.CreateNoWindow         = $true
        $processInfo.RedirectStandardOutput = $true

        # Assign ProcessStartInfo to a process
        $process           = [System.Diagnostics.Process]::new()
        $process.StartInfo = $processInfo
    
        # Start process
        $process.Start()
        Start-Sleep -Milliseconds 300

        while ($null -ne ($standard_output = $process.StandardOutput.ReadLine()))
        {
            if ($standard_output -match '\S+\spid:') 
            {
                $exe = $standard_output
            } 
            elseif ($standard_output -like "*$($path)*")  { 
                Write-Host "$exe - $standard_output"
            }
        }

        # Check if process was finished successfully
        if(!$process.HasExited)
        {
            Write-Error "Process of `"handle.exe`" was not finished."
            exit -1
        }

        $process.Dispose()
    }
    catch 
    {
        Write-Error "Error during execution of `"handle.exe`"`n$($_Exception.Message)"
    }

    Read-Host "`n`nContinue with the enter key..."
    exit 0
}