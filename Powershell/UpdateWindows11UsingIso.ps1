<#
    ### Description ###
    Small script to execute the Windows 11 Ugrade, or install the Functional Updates, if the pc does not have the Requirements.  

    ### Tutorial ###
    Link-Source: https://www.youtube.com/watch?v=5NSEUcveHoE

    ### Windows ISO/Functional Update ISO ###
    Source-Link: https://www.microsoft.com/de-de/software-download/windows11

    ### Prerequisites ###
    ISO File for upgrade to Windows 11, or functional update is already downloaded and mounted.

#>

param
(
    [string] $IsoPath
)

function Main
{
    [boolean] $continue = $false
    do
    {
        Write-Host "`n`n### Start Windows 11 Upgrade from ISO ###"
    
        if([string]::IsNullOrEmpty($IsoPath))
        {
            Write-Information "`nTo exit the script, just type (e)xit in the console-input!"    
            $IsoPath = Read-Host "Iso-Path"

            if(![string]::IsNullOrEmpty($IsoPath))
            {
                $IsoPath = $IsoPath.Trim().Trim("`"")
                if($IsoPath.ToUpper() -eq "E" -or $IsoPath.ToUpper() -eq "EXIT")
                {
                    Write-Host "`nScript will be exited."
                    Read-Host "`n`nContinue with the Enter-Key"
                    exit 1
                }

                $continue = ValidatePath($IsoPath)
            }
            else
            {
                Write-Error "The given Iso-Path is NULL or Empty!"
                Read-Host "`n`nContinue with the Enter-Key"
            }
        }
        else
        {
            $continue = ValidatePath($IsoPath)
        }

        Clear-Host

    } while (!$continue)

    & $IsoPath /product server
}

# Validates Input
function ValidatePath([string] $path)
{
    if(Test-Path -Path $path)
    {
        return $true
    }
    else
    {
        Write-Error "The given Iso-Path `"$($path)`" does NOT exist!"
        Read-Host "`n`nContinue with the Enter-Key"
        return $false
    }
}

Main