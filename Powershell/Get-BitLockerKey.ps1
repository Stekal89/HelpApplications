$BitlockerVolumers = Get-BitLockerVolume
$BitlockerVolumers | ForEach-Object {
    $MountPoint = $_.MountPoint
    $RecoveryKey = [string]($_.KeyProtector).RecoveryPassword
    if ($RecoveryKey.Length -gt 5) {                                             
        Write-Output ("The drive $MountPoint has a recovery key $RecoveryKey.")
    }
}

