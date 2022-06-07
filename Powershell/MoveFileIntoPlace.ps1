
#region Functions

<#
.DESCRIPTION
Lock console and continue after pressing any key
#>
Function AnyKeyDown{
    Write-Host "`n`nContinue with any key..."
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
}

<#
.DESCRIPTION
Try to parse a string into a integer

.PARAMETER stringAsNumber
Text which should be parsed

.NOTES
return -> parsed Integer/NULL
#>
Function IntTryParse {
    param([string] $stringAsNumber, [switch] $errorOutput)

    [int] $returnNumber = $null

    if (!([int32]::TryParse($stringAsNumber , [ref]$returnNumber))) {
        if ($errorOutput) {
            [string] $erOut = "Cannot parse string to Integer!`n"
            $erOut += "String to parse: `"$($stringAsNumber)`""
            Write-Error $erOut    
        }
        return $null
    }

    return $returnNumber
}

<#
.DESCRIPTION
Get the filepath of the file which should be moved by user interaction

.PARAMETER defaultOutput
Ref defaultOutput -> Default-Output which should be shown if the console-output will be cleared.

.NOTES
return -> FilePath of the file which should be moved
#>
Function GetFilePathFromUser {
    param ([ref]$defaultOutput)

    [string] $fileToMove = $null

    [bool] $inputSuccess = $false
    do{
        $fileToMove = Read-Host "Please enter the file-path (file which should be moved)"

        $fileToMove = $fileToMove.Replace('"', '').Replace("[", "``[").Replace("]", "``]")
        # Validate Input
        if ([String]::IsNullOrEmpty($fileToMove)) {
            Write-Error "Parameter empty!`nParameter: `"fileToMove`""
            AnyKeyDown
            Clear-Host        
        }
        else {
            if (!(Test-Path -Path $fileToMove -ErrorAction SilentlyContinue)) {
                Write-Error "File which should be moved does not exist!`nFileToMove: `"$($fileToMove)`""
                AnyKeyDown
                Clear-Host
            }
            else {
                $defaultOutput.Value = "file-path: `"$($fileToMove)`""
                $inputSuccess = $true
            }
        }
    } while (!$inputSuccess)

    return $fileToMove
}

<#
.DESCRIPTION
Get the destination where the file should be located by user interaction

.PARAMETER defaultOutput
Ref defaultOutput -> Default-Output which should be shown if the console-output will be cleared.

.NOTES
return -> destination
#>
Function GetDestinationFromUser {
    param ([ref]$defaultOutput)

    [string] $destination = $null
    [bool] $inputSuccess = $false    
    do{
        [string] $destination = Read-Host "Please enter the destination"

        $destination = $destination.Replace('"', '')
        # Validate Input
        if ([String]::IsNullOrEmpty($destination)) {
            Write-Error "Parameter empty!`nParameter: `"destination`""
            AnyKeyDown
            Clear-Host
            Write-Host $defaultOutput.Value
        }
        else {
            if (!(Test-Path -Path $destination -ErrorAction SilentlyContinue)) {
                Write-Error "Destinationpath does not exist!`ndestination: `"$($destination)`""
                AnyKeyDown
                Clear-Host
                Write-Host $defaultOutput.Value
            }
            else {
                $defaultOutput.Value += "`n`ndestination: `"$($destination)`""
                $inputSuccess = $true
            }
        }
        
    } while (!$inputSuccess)

    return $destination
}

<#
.DESCRIPTION
Get the new position of the file.

.PARAMETER defaultOutput
Ref defaultOutput -> Default-Output which should be shown if the console-output will be cleared.

.NOTES
return -> Position as Integer
#>
Function GetPositionFromUser {
    param([ref]$defaultOutput)

    [bool] $inputSuccess = $false
    [int] $pos = $null

    do{
        [string] $position = Read-Host "Please enter the wished position of the file"

        $position = $position.Replace('"', '')
        # Validate Input
        if ([String]::IsNullOrEmpty($position)) {
            Write-Error "Parameter empty!`nParameter: `"position`""
            AnyKeyDown
            Clear-Host
            Write-Host $defaultOutput.Value
        }
        else {
            # Validate if input is a integer:
            $pos = IntTryParse -StringAsNumber $position -errorOutput
            if ($null -eq $pos) {
                Write-Error "Input is not a valid number!`nPosition: `"$($position)`""
                AnyKeyDown
                Clear-Host
                Write-Host $defaultOutput.Value
            }
            else {
                $inputSuccess = $true
            }
        }
    } while (!$inputSuccess)

    return $pos
}

<#
.DESCRIPTION
Fixes positions/numbers of files

.PARAMETER destination
destination -> Destinationpath where the positions should be fixed
#>
Function FixEmptyPositions {
    param([string] $destination)

    Write-Host "`nLoad and fix the empty positions/numbers"

    $files = Get-ChildItem -Path $destination -Force | Where-Object { $_.Extension -eq ".mp3" } | Sort-Object -Property "Name"

    if ($null -eq $files -or $files.Count -le 0) {
        Write-Warning "No already existing files found in the destination `"$($destination)`""
        AnyKeyDown
        Clear-Host
    }
    else {

        [int] $lastPosition = 0

        for ($i = 0; $i -lt $files.Count; $i++) {
            try {
                [string[]] $fileAr = $files[$i].Name.Split('_')

                if ($null -ne $fileAr -and $fileAr.Count -gt 0) {
                    [int] $actNumb = IntTryParse -stringAsNumber $fileAr[0]
                    
                    # Get Filename without number-prefix
                    [string] $fileNameWithoutPrefix = $null

                    for ($j = 1; $j -lt $fileAr.Count; $j++) {
                        $fileNameWithoutPrefix += "$($fileAr[$j])_"
                    }

                    $fileNameWithoutPrefix = $fileNameWithoutPrefix.TrimEnd('_')

                    if ($null -ne $actNumb -and $actNumb -ge 0) {
                    
                        if ($actNumb -gt ($lastPosition + 1)) {
                            Rename-Item -Path $files[$i].FullName -NewName "$("{0:D3}" -f ($lastPosition + 1))_$($fileNameWithoutPrefix.Replace("[", "``[").Replace("]", "``]"))" -Force -Verbose
                        }

                        $lastPosition ++
                    }
                }
            }
            catch {
                Write-Error "FixEmptyPositions-Error: $($_.Exception.Message)"
                AnyKeyDown
            }
        }
    }
}

<#
.DESCRIPTION
Increase all Positions, which are greater, or equal than the positon which was defined by the user

.PARAMETER destination
destination -> Destinationpath where the positions should setted
#>
Function SetPositionsOfFiles {
    param([string] $destination)

    Write-Host "`nSet positions of files."

    # Load all existing files from directory
    $files = Get-ChildItem -Path $destination -Force | Where-Object { $_.Extension -eq ".mp3" } | Sort-Object -Property "Name"

    if ($null -eq $files -or $files.Count -le 0) {
        Write-Warning "No already existing files found in the destination `"$($destination)`""
        AnyKeyDown
        Clear-Host
    }
    else {
        foreach($item in $files) {
            try {
                [string[]] $fileAr = $item.Name.Split('_')

                if ($null -ne $fileAr -and $fileAr.Count -gt 0) {
                    [int] $actNumb = IntTryParse -stringAsNumber $fileAr[0]
                    
                    # Get Filename without number-prefix
                    [string] $fileNameWithoutPrefix = $null

                    for ($i = 1; $i -lt $fileAr.Count; $i++) {
                        $fileNameWithoutPrefix += "$($fileAr[$i])_"
                    }

                    $fileNameWithoutPrefix = $fileNameWithoutPrefix.TrimEnd('_')

                    if ($null -ne $actNumb -and $actNumb -ge $pos) {
                        Rename-Item -Path $item.FullName -NewName "$("{0:D3}" -f ($actNumb + 1))_$($fileNameWithoutPrefix.Replace("[", "``[").Replace("]", "``]"))" -Force -Verbose
                    }
                }
            }
            catch {
                Write-Error "SetPositionsOfFiles-Error: $($_.Exception.Message)"
                AnyKeyDown
            }
        }
    }
}

<#
.DESCRIPTION
Move the file to the destination and rename the filename if neccessary.

.PARAMETER fileToMove
Parameter description

.PARAMETER destination
Parameter description

.PARAMETER pos
Parameter description

.EXAMPLE
An example

.NOTES
General notes
#>
Function MoveAndRenameFile {
    param([string] $fileToMove, [string] $destination, [int] $pos)

    Write-Host "`nMove file to destination and rename it..."
    try {
         # Verify file-name of the file which should be moved and rename it if neccessary
        [string] $fileName = Split-Path $fileToMove -leaf
        [string[]] $fileAr = $fileName.Split('_')

        if ($null -ne $fileAr -and $fileAr.Count -gt 0) {
            [System.Nullable``1[[System.Int32]]] $actNumb = IntTryParse -stringAsNumber $fileAr[0]

            if ($null -ne $actNumb) {

                # Get Filename without number-prefix
                [string] $fileNameWithoutPrefix = $null

                for ($i = 1; $i -lt $fileAr.Count; $i++) {
                    $fileNameWithoutPrefix += "$($fileAr[$i])_"
                }

                $fileNameWithoutPrefix = $fileNameWithoutPrefix.TrimEnd('_')

                Move-Item -Path $fileToMove.Replace("[", "``[").Replace("]", "``]") -Destination $destination -Force -Verbose

                if ($actNumb -ne $pos) {
                    #Format the integer to the correct format with 0 before the numb, if the number is too short
                    # Example: 
                    # Input is 70
                    # Call: "{0:D5}" -f $pos
                    #
                    # Output: "00070"
                    
                    Rename-Item -Path (Join-Path $destination $fileName.Replace("[", "``[").Replace("]", "``]")) -NewName "$("{0:D3}" -f $pos)_$($fileNameWithoutPrefix)" -Force -Verbose
                    #Rename-Item -Path (Join-Path $destination $fileName) -NewName "$("{0:D3}" -f $pos)_$($fileNameWithoutPrefix.Replace("[", "``[").Replace("]", "``]"))" -Force -Verbose
                }
            }
            else {
                [string] $fileNameWithoutPrefix = $fileToMove
                
                Move-Item -Path $fileToMove -Destination $destination -Force
                #Rename-Item -Path (Join-Path $destination $fileName) -NewName "$("{0:D3}" -f $pos)_$($fileName.Replace("[", "``[").Replace("]", "``]"))" -Force -Verbose
                Rename-Item -Path (Join-Path $destination $fileName.Replace("[", "``[").Replace("]", "``]")) -NewName "$("{0:D3}" -f $pos)_$($fileName)" -Force -Verbose
            }
        }
    }
    catch {
        Write-Error "MoveAndRenameFile-Error: $($_.Exception.Message)"
        AnyKeyDown
    }
   
}

#endregion

#region Main

[string] $defaultOutput = ""

#region Get inputinformation from user

[string] $fileToMove = GetFilePathFromUser -defaultOutput ([ref]$defaultOutput)
if ([string]::IsNullOrEmpty($fileToMove)) {
    Write-Error "FilePath is null, or empty!"
    AnyKeyDown
    exit 1
}

[string] $destination = GetDestinationFromUser -defaultOutput ([ref]$defaultOutput)
if ([string]::IsNullOrEmpty($destination)) {
    Write-Error "Destination is null, or empty!"
    AnyKeyDown
    exit 1
}

[int] $pos = GetPositionFromUser -defaultOutput ([ref]$defaultOutput)
if ($null -eq $pos -or $pos -le 0) {
    Write-Error "Position has an not valid value!`nSupported values are greater than 0."
    AnyKeyDown
    exit 1
}

#endregion

# Load and fix the empty positions/numbers
FixEmptyPositions -destination $destination

# Increase all Positions, which are greater, or equal than the positon which was defined by the user
SetPositionsOfFiles -destination $destination

# Move file in destination and rename it if neccessary
MoveAndRenameFile -fileToMove $fileToMove -destination $destination -pos $pos

AnyKeyDown
exit 0

#endregion