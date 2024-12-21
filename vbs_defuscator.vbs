' https://isvbscriptdead.com/vbs-obfuscator/ 
Option Explicit 
Function Defuscator(vbs) 
    Dim codeStart, codeEnd, code, pattern 
    ' Define patterns for different obfuscation techniques 
    Dim patterns 
    patterns = Array("Execute", "CLng(&H", "Function l(", "Function r(") 
    ' Iterate through patterns to find the obfuscation type 
    For Each pattern In patterns 
        codeStart = InStr(1, vbs, pattern, 1) 
        If codeStart > 0 Then 
            Exit For 
        End If 
    Next 
    If codeStart > 0 Then 
        ' Extract the code following the identified pattern 
        codeStart = codeStart + Len(pattern) 
        codeEnd = InStr(codeStart, vbs, vbCrLf) ' Find end of line or statement 
        If codeEnd = 0 Then 
            codeEnd = Len(vbs) ' If no end of line, use end of string 
        End If 
        code = Trim(Mid(vbs, codeStart, codeEnd - codeStart)) 
        ' Evaluate and return the result 
        On Error Resume Next 
        Defuscator = Eval(code) 
        If Err.Number <> 0 Then 
            WScript.Echo "Error evaluating code: " & Err.Description 
            Err.Clear 
        End If 
        On Error GoTo 0 
    Else 
        ' If no identifiable pattern found, return original 
        Defuscator = vbs 
    End If 
End Function 
Sub Main() 
    Dim fso, i 
    Const ForReading = 1 
    Set fso = CreateObject("Scripting.FileSystemObject") 
    If WScript.Arguments.Count = 0 Then 
        WScript.Echo "Usage: cscript vbs_defuscator.vbs <file1> <file2> ..." 
        WScript.Quit 
    End If 
    For i = 0 To WScript.Arguments.Count - 1 
        Dim FileName 
        FileName = WScript.Arguments(i) 
        If fso.FileExists(FileName) Then 
            Dim MyFile 
            Set MyFile = fso.OpenTextFile(FileName, ForReading) 
            Dim vbs 
            vbs = MyFile.ReadAll 
            WScript.Echo "De-obfuscating file: " & FileName 
            WScript.Echo Defuscator(vbs) 
            MyFile.Close 
        Else 
            WScript.Echo "File not found: " & FileName 
        End If 
    Next 
    Set fso = Nothing 
End Sub 
Call Main()