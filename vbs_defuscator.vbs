' vbs_defuscator.vbs 
Option Explicit 
' Include logger 
ExecuteGlobal CreateObject("Scripting.FileSystemObject").OpenTextFile("logger.vbs", 1).ReadAll 
Function Defuscator(vbs) 
    Dim t, result 
    On Error Resume Next 
    t = InStr(1, vbs, "Execute", 1) 
    If Err.Number <> 0 Or t = 0 Then 
        LogMessage "Error finding 'Execute' in input or 'Execute' not found: " & vbs & " - " & Err.Description 
        Err.Clear 
        Defuscator = "" 
        Exit Function 
    End If 
    t = Mid(vbs, t + Len("Execute")) 
    If Err.Number <> 0 Then 
        LogMessage "Error extracting code after 'Execute': " & vbs & " - " & Err.Description 
        Err.Clear 
        Defuscator = "" 
        Exit Function 
    End If 
    result = Eval(t) 
    If Err.Number <> 0 Then 
        LogMessage "Error evaluating code: " & t & " - " & Err.Description 
        Err.Clear 
        Defuscator = "" 
        Exit Function 
    End If 
    Defuscator = result 
End Function 
Dim fso, i 
Const ForReading = 1 
Set fso = CreateObject("Scripting.FileSystemObject") 
LogMessage "Script started." 
For i = 0 To WScript.Arguments.Count - 1 
    Dim FileName 
    FileName = WScript.Arguments(i) 
    Dim MyFile 
    On Error Resume Next 
    Set MyFile = fso.OpenTextFile(FileName, ForReading) 
    If Err.Number <> 0 Then 
        LogMessage "Error reading file: " & FileName & " - " & Err.Description 
        Err.Clear 
        Continue For 
    End If 
    On Error GoTo 0 
    Dim vbs 
    vbs = MyFile.ReadAll 
    LogMessage "Read file: " & FileName 
    Dim result 
    result = Defuscator(vbs) 
    WScript.Echo result 
    LogMessage "De-obfuscation result for " & FileName & ": " & result 
    MyFile.Close 
Next 
LogMessage "Script ended." 
Set fso = Nothing