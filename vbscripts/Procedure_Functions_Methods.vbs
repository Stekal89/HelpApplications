' Procedure & Functions/Methods

' # # # # # S U B S # # # # # -> (Procedure's) are without return values!

' Without Parameter -> Sub
Sub SubWithoutParameterWithoutReturnValue()
	' This variables musst be declared outside of the Procedure!
	a = 5 + b
	WScript.Echo "a = 5 + b"
	WScript.Echo """" & a & """ = 5 + """ & b & """"
	WScript.Echo ""
	
	WScript.Echo "Inside Sub: """ & a & """"
End Sub

' Call of The Procedure "SubWithoutParameterWithoutReturnValue"
Dim a, b
b = 3
WScript.Echo "###########################################"
WScript.Echo "Sub: ""SubWithoutParameterWithoutReturnValue"""
WScript.Echo "Before Call:"
WScript.Echo "a: """ & a & """"
WScript.Echo "b: """ & b & """"
WScript.Echo ""

SubWithoutParameterWithoutReturnValue()
' a will be also changed outside of the Sub
WScript.Echo "Outside Sub: """ & a & """"
WScript.Echo "###########################################"
WScript.Echo ""

' ####################################################################################################################################################################################

' !!!!!!!!!!!!! Subs with parameter is not working!!!!!!!!!!!!!
' It is possible to make a input box inside of the Sub
' With Parameter -> Sub
'Sub SubWithoutReturnValue(firstValue, secondValue)
'	first = 5 + secondValue
'	WScript.Echo "firstValue = 5 + secondValue"
'	WScript.Echo """" & firstValue & """ = 5 + """ & secondValue & """"
'	WScript.Echo ""

'	WScript.Echo "Inside Sub: """ & firstValue & """"
'End Sub

' Call of The Procedure "SubWithoutReturnValue"
'Dim c, d
'c = 2
'd = 4
'WScript.Echo "###########################################"
'WScript.Echo "Sub: ""SubWithoutReturnValue"""
'SubWithoutReturnValue(2, 3)
'WScript.Echo """c"" Outside Sub: """ & c & """"
'WScript.Echo """d"" Outside Sub: """ & d & """"
'WScript.Echo """firstValue"" Outside Sub: """ & first & """"
'WScript.Echo """secondValue"" Outside Sub: """ & secondValue & """"
'WScript.Echo "###########################################"
'WScript.Echo ""

' ####################################################################################################################################################################################

' # # # # # F U N C T I O N S # # # # # -> (Functions/Methods) are with return values!

' Function without Parameters
Function FunctionWithoutParameters()
	'In this case the variable "fa" should exist outside of this function!
	temp = 5 + fa
	WScript.Echo "temp = 5 + fa"
	WScript.Echo """" & temp & """ = 5 + """ & fa & """"
	WScript.Echo ""
	
	WScript.Echo """fa"" Outside: """ & fa & """"
	WScript.Echo """fb"" Outside: """ & fb & """"  'NULL/Not exist
	WScript.Echo """temp"" Outside: """ & temp & """"
	WScript.Echo ""
	
	' return
	FunctionWithoutParameters = temp
End Function
Dim fa, fb
fa = 3
WScript.Echo "###########################################"
WScript.Echo "Function: ""FunctionWithoutParameters"""
WScript.Echo "Before Call:"
WScript.Echo "fa: """ & fa & """"
WScript.Echo "fb: """ & fb & """" ' NULL
WScript.Echo ""

fb = FunctionWithoutParameters()
WScript.Echo """fa"" Outside: """ & fa & """"
WScript.Echo """fb"" Outside: """ & fb & """"
WScript.Echo """temp"" Outside: """ & temp & """" ' NULL/Not exist
WScript.Echo "###########################################"
WScript.Echo ""

' ####################################################################################################################################################################################


' Function with Parameters:
' If outside a variable called tempRes exist, it will be overwritten!!!!
Function FunctionWithNormalParameter(v1, v2, ex)
	tempRes = v1 + v2
	
	WScript.Echo "tempRes = v1 + v2"
	WScript.Echo """" & tempRes & """ = """ & v1 & """ + """ & v2 &""""
	WScript.Echo ""
	
	If ex Then
		WScript.Echo """f"" Inside: """ & f & """"
		WScript.Echo """s"" Insied: """ & s & """"
	End If
	WScript.Echo """r"" Inside: """ & r & """" ' NULL
	WScript.Echo """v1"" Inside: """ & v1 & """"
	WScript.Echo """v2"" Inside: """ & v2 & """"
	WScript.Echo """tempRes"" Inside: """ & tempRes & """" 
	WScript.Echo ""
	
	FunctionWithNormalParameter = tempRes
End Function

Dim f, s, r
f = 3
s = 7
WScript.Echo "###########################################"
WScript.Echo "Function: ""FunctionWithoutParameters"" -> Different Variables"
WScript.Echo "Before Call:"
WScript.Echo "f: """ & f & """"
WScript.Echo "s: """ & s & """" 
WScript.Echo "r: """ & r & """" ' NULL
WScript.Echo ""

r = FunctionWithNormalParameter(f, s, True)

WScript.Echo """f"" Outside: """ & f & """"
WScript.Echo """s"" Outside: """ & s & """"
WScript.Echo """r"" Outside: """ & r & """" 
WScript.Echo """v1"" Outside: """ & v1 & """" ' NULL
WScript.Echo """v2"" Outside: """ & v2 & """" ' NULL
WScript.Echo """tempRes"" Outside: """ & tempRes & """" ' NULL
WScript.Echo "###########################################"
WScript.Echo ""

' ##########################################################################################
' if the variable exists outside, which is also included in the function, it will be overwritten

Dim v1, v2, tempRes
v1 = 3
v2 = 7
r = 0

WScript.Echo "###########################################"
WScript.Echo "Function: ""FunctionWithoutParameters"" -> Same Variables"
WScript.Echo "Before Call:"
WScript.Echo "v1: """ & v1 & """"
WScript.Echo "v2: """ & v2 & """" 
WScript.Echo "r: """ & r & """"
WScript.Echo "tempRes: """ & tempRes & """" ' NULL
WScript.Echo ""

r = FunctionWithNormalParameter(v1, v2, false)

WScript.Echo """v1"" Outside: """ & v1 & """" 
WScript.Echo """v2"" Outside: """ & v2 & """" 
WScript.Echo """r"" Outside: """ & r & """" 
WScript.Echo """tempRes"" Outside: """ & tempRes & """" 
WScript.Echo "###########################################"
WScript.Echo ""

' ####################################################################################################################################################################################

' Function with value-parameters
' takes the only the value of the variable, so the variable gets not touched outside! 
Function FunctionWithValueParameters(ByVal val)
	WScript.Echo "val = val^2"
	WScript.Echo """" & val^2 & """ = """ & val & """^2"
	WScript.Echo ""
	val = val^2
	
	WScript.Echo """val"" Inside: """ & val & """" 
	WScript.Echo ""
	FunctionWithValueParameters = val
End Function

Dim val, res
val = 3

WScript.Echo "###########################################"
WScript.Echo "Function: ""FunctionWithoutParameters"" -> Same Variables"
WScript.Echo "Before Call:"
WScript.Echo "val: """ & val & """"
WScript.Echo "res: """ & res & """" ' NULL
WScript.Echo ""

res = FunctionWithValueParameters(val)

WScript.Echo """val"" Outside: """ & val & """" 
WScript.Echo """res"" Outside: """ & res & """" 
WScript.Echo "###########################################"
WScript.Echo ""
 
' ####################################################################################################################################################################################
' Function with Reference parameter:
' references a variable and overwrites the value of the variable
Function FunctionWithReferenceParameters(ByRef	val2)
	WScript.Echo "val2 = val^2"
	WScript.Echo """" & val2^2 & """ = """ & val2 & """^2"
	WScript.Echo ""
	val2 = val2^2
	
	WScript.Echo """val2"" Inside: """ & val2 & """" 
	WScript.Echo ""
	FunctionWithReferenceParameters = val2
End Function

Dim val2, val3, result1, result2
val2 = 3
val3 = 5

WScript.Echo "###########################################"
WScript.Echo "Function: ""FunctionWithoutParameters"" -> Same Variables"
WScript.Echo "Before Call:"
WScript.Echo "val2: """ & val2 & """"
WScript.Echo "val3: """ & val3 & """"
WScript.Echo "result1: """ & result1 & """" ' NULL
WScript.Echo "result2: """ & result2 & """" ' NULL
WScript.Echo ""

result1 = FunctionWithReferenceParameters(val2)
result2 = FunctionWithReferenceParameters(val3)

WScript.Echo """val2"" Outside: """ & val2 & """" ' Overwritten and the same like "res1"
WScript.Echo """val3"" Outside: """ & val3 & """" ' Overwritten and the same like "res2"
WScript.Echo """result1"" Outside: """ & result1 & """" ' Overwritten and the same like "val2"
WScript.Echo """result2"" Outside: """ & result2 & """" ' Overwritten and the same like "val3"
WScript.Echo "###########################################"
WScript.Echo ""

WScript.Echo "Break"