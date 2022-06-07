Attribute VB_Name = "CellValueToUpper"

' Example to convert text ToUpper
Private Sub Worksheet_Change(ByVal Target As Range)
    Dim strAddress As String: strAddress = Target.Address
    If Not AuftragNeuanlage.IsNullOrEmpty(strAddress) Then
        If InStr(1, strAddress, "$C$") = 1 Then
            Dim arSplitted: arSplitted = Split(Right(strAddress, Len(strAddress) - CLng(1)), "$")
            Dim nCount As Integer: nCount = UBound(arSplitted) + 1
            If nCount > 1 Then
                Dim strRow As String: strRow = arSplitted(1)
                If IsNumeric(strRow) Then
                    Dim nRow As Long: nRow = CLng(strRow)
                    If nRow >= CLng(10) Then
                        Target.Value = UCase(CStr(Target.Value))
                    End If
                End If
            End If
        End If
    End If
End Sub
