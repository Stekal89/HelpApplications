

Dim d, strDates
strDateAsInput = InputBox("Bitte Datum eingeben:", "Parameter1 (Datum)")

d=CDate(strDateAsInput)

'0 = vbGeneralDate - Default. Returns date: mm/dd/yy and time if specified: hh:mm:ss PM/AM.
'1 = vbLongDate - Returns date: weekday, monthname, year
'2 = vbShortDate - Returns date: mm/dd/yy
'3 = vbLongTime - Returns time: hh:mm:ss PM/AM
'4 = vbShortTime - Return time: hh:mm

'd=CDate("2010-02-16 13:45")
strDates = "Datum: " & FormatDateTime(d)
strDates = strDates & vbNewLine & "Datum1: " & FormatDateTime(d,1)
strDates = strDates & vbNewLine & "Datum2: " & FormatDateTime(d,2)
strDates = strDates & vbNewLine & "Datum3: " & FormatDateTime(d,3)
strDates = strDates & vbNewLine & "Datum4: " & FormatDateTime(d,4)
strDates = strDates & vbNewLine & "Datum5: " & FormatString("{0:yyyyMMdd hh:mm:ss}", Array(d))

WScript.Echo strDates

' F U N C T I O N
' Formats a string into specified format
' param: strStringFormat -> StringFormat e. g.: {0:yyyyMMddhhmmss}
' param: strData -> Data which should be formatted into the specified Format
' return: Formatted string
Function FormatString(strStringFormat, strData)
	Dim objStringBuilder : Set objStringBuilder = CreateObject("System.Text.StringBuilder")
	
   	objStringBuilder.AppendFormat_4 strStringFormat, (strData)
   	FormatString = objStringBuilder.ToString()
   	
   	objStringBuilder.Length = 0
   	'objStringBuilder = Nothing
End Function