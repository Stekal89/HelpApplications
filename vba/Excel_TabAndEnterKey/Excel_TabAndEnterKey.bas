Attribute VB_Name = "Excel_TabAndEnterKey"
' SHEET:
'Private Sub Worksheet_Activate()
'    Application.OnKey "{TAB}", "BestellungNeuanlage.TabKeyHandler"
'    Application.OnKey "{ENTER}", "BestellungNeuanlage.EnterKeyHandler"
'End Sub


' MODULE:
'Private Sub TabKeyHandler()
'    If Selection.Cells.Address = "$B$3" Then
'        Debug.Print "TabKeyHandler"
'    Else
'        ' Default Excel-Behavior
'        If Not InStr(ActiveCell.Address, ":") Then
'            ActiveCell.Offset(0, 1).Select
'        Else
'
'        End If
'        'ActiveCell.Offset(0, 1).Select '.Range("A1").Select
'    End If
'End Sub

'Private Sub EnterKeyHandler()
'    If Selection.Cells.Address = "$B$3" Then
'        Debug.Print "EnterKeyHandler"
'    Else
'        'Move Down a Row, same Column use:
'        Call ActiveCell.Offset(1, 0)
'
'        'Move Up a Row, same Column:
'        'ActiveCell.Offset(-1, 0)
'
'        'Move to Next Column, same Row:
'        'ActiveCell.Offset(0, 1)
'
'        'Move to Previous Coulmn, same Row:
'        'ActiveCell.Offset(0, -1)
'    End If
'End Sub
