param 
(
    [String] $MsBuildExe = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\amd64\MSBuild.exe",
    #[parameter(Mandatory=$true)]
    [String] $Path,
    [String] $DestinationPath,
    #[parameter(Mandatory=$true)]
    [Switch] $Nuget = $true, 
    #[parameter(Mandatory=$true)]
    [Switch] $Clean = $true
)

#Region Parameter Validation

[System.IO.FileInfo] $slnPath = $null

Write-Host "Validate Parameter Path..."
if ([String]::IsNullOrEmpty($Path)) 
{
    Write-Host "Path is not defined, so use the PSScriptRoot path and search for the sln file..."
    $slnPath = Get-ChildItem -Path "$($PSScriptRoot)" -Recurse | Where-Object { $_.Extension -eq ".sln" }
}
else 
{
    $slnPath = $Path
}

if ([String]::IsNullOrEmpty($slnPath)) 
{
    Write-Error "`nSolutionpath is NULL or EMPTY!"
    exit 1
}

Write-Host "Verify extension of the Solutionpath..."
if (!($slnPath.FullName.EndsWith(".sln"))) 
{
    Write-Error "`nSolutionpath does not end with `"*.sln`"`nSolutionpath: `"$($slnPaht)`""
    exit 1
}

Write-Host "Check if path `"$($slnPath)`" for the solution exists..."
if (!(Test-Path -Path $slnPath.FullName -Verbose))  
{
    Write-Error "Solutionpath `"$($slnPath.FullName)`" does not exist..."
    exit 1
}

if ([String]::IsNullOrEmpty($DestinationPath)) 
{
    Write-Error "`nParameter DestinationPath is NULL or Empty!"
    exit 1    
}

Write-Host "Check if destinationpath `"$($DestinationPath)`" exists..."
if (!(Test-Path -Path $DestinationPath))
{
    Write-Error "Destinationpath `"$($DestinationPath)`" deos not exist!"
    exit 1
}

#Endregion

#Region Build

try 
{
    if ($Nuget) 
    {
        Write-Host "`nRestoring Nuget packages" -ForegroundColor Green

        nuget restore "$($slnPath.FullName)"
    
        if ($LASTEXITCODE -ne 0) 
        {
            Write-Error "`n`nError during restoring of Nuget packages!
            ExitCode: `"$($LASTEXITCODE)`""
            exit 1
        }
    }

    if ($clean) 
    {
        Write-Host "`nCleaning `"$($slnPath.FullName)`"" -ForegroundColor Green
        & "$($msBuildExe)" "$($slnPath.FullName)" ` /t:Clean /m /p:Configuration=Release

        if ($LASTEXITCODE -ne 0) 
        {
            Write-Error "`n`nError during clean of output directory!`nExitCode: `"$($LASTEXITCODE)`""
            exit 1
        }
    }

    Write-Host "`nBuilding `"$($slnPath.FullName)`"" -ForegroundColor Green
 
    & "$($msBuildExe)" "$($slnPath.FullName)" /t:Build /m /p:Configuration=Release
    
    if ($LASTEXITCODE -ne 0) 
    {
        Write-Error "`n`nError during clean of output directory!
        ExitCode: `"$($LASTEXITCODE)`""
        exit 1
    }
}
catch 
{
    Write-Error "Error durng building of solution $($_.Exception.Message)"
    exit 1
}

#Endregion

#Region Copy and Rename Binaries

# Verify exe exist
[System.IO.FileInfo[]] $binaries = Get-ChildItem -Path "$($PSScriptRoot)" -Recurse | Where-Object { $_.FullName -like "*bin\x86\Release*" -and $_.Extension -eq ".exe" }
if ($null -eq $binaries) 
{
    Write-Error "No binaries are found!
    $_.FullName -like `"*bin\x86\Release*`"
    and $_.Extension -eq `".exe`""
    exit 1
}

# Copy and rename exe in the Destinationpath

foreach($binary in $binaries)
{
    [System.Diagnostics.FileVersionInfo] $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$($binary.FullName)")

    # Get folderinformations
    [string] $strDestination = (Join-Path $DestinationPath "$($version.FileMajorPart).$($version.FileMinorPart)")

    if ([String]::IsNullOrEmpty($strDestination))
    {
        Write-Error "`nVersion-Destinationpath is NULL or Empty!"
        exit 1
    }

    Write-Host "`nCheck if Version-Destinationpath exists..."
    if (!(Test-Path -Path $strDestination -Verbose)) 
    {
        New-Item -Path $strDestination -ItemType Directory -Verbose -Force
    }

    [string] $strNewName = $null    
    $strNewName = "$($binary.Name.TrimEnd(".exe"))_$($version.FileMajorPart).$($version.FileMinorPart)$($binary.Extension)"

    [boolean] $bCopyFile = $true
    # Load fileinfo of destinationfile if already exist
    if (Test-Path -Path (Join-Path $strDestination $strNewFileName)) 
    {
        [System.IO.FileInfo] $actDestinationFile = Get-ChildItem -Path "$($strDestination)" -Recurse | Where-Object { $_.Name -eq $strNewName }

        # If builded file is newer
        if ($null -ne $actDestinationFile)
        {
            if ($actDestinationFile.CreationTime -le $binary.CreationTime)
            {
                Remove-Item -Path "$($actDestinationFile.FullName)" -Force -Verbose
                $bCopyFile = $true
            }
        }
    }

    if ($bCopyFile)
    {    
        Write-Host "`n`nExecute following command to copy the binary:`n"
        Write-Host "robocopy.exe "`"$($binary.Directory)`"" "`"$($strDestination)`"" "`"$($binary.Name)`""`n`n"
        robocopy.exe "`"$($binary.Directory)`"" "`"$($strDestination)`"" "`"$($binary.Name)`"" #/ZB /COPY:DAT /R:0 /W:0 /ndl /njs #/nfl /nc /ns

        Write-Host "Rename the copied file directly in the destinationfolder..."
        Rename-Item -Path (Join-Path "$($strDestination)" "$($binary.Name)") -NewName $strNewName -Force -Verbose
    }
}

Read-Host "Close with the Enter key..."

#Endregion