Option Explicit 
Function Defuscator(vbs) 
    On Error Resume Next 
    Dim t, scriptStart 
    scriptStart = InStr(1, vbs, "Execute", 1) 
    If Err.Number <> 0 Or scriptStart = 0 Then 
        LogError "Error in finding 'Execute' in script: " & Err.Description 
        Exit Function 
    End If 
    t = Trim(Mid(vbs, scriptStart + Len("Execute"))) 
    If Err.Number <> 0 Then 
        LogError "Error in extracting script part: " & Err.Description 
        Exit Function 
    End If 
    t = Eval(t) 
    If Err.Number <> 0 Then 
        LogError "Error in evaluating script: " & Err.Description 
        Exit Function 
    End If 
    LogStatus "Deobfuscation successful." 
    Defuscator = t 
End Function 
Sub LogError(message) 
    Dim logFile 
    Set logFile = fso.OpenTextFile("error_log.txt", 8, True) 
    logFile.WriteLine Now & " - ERROR - " & message 
    logFile.Close 
End Sub 
Sub LogStatus(message) 
    Dim logFile 
    Set logFile = fso.OpenTextFile("status_log.txt", 8, True) 
    logFile.WriteLine Now & " - STATUS - " & message 
    logFile.Close 
End Sub 
Dim fso, i 
Const ForReading = 1 
Set fso = CreateObject("Scripting.FileSystemObject") 
For i = 0 To WScript.Arguments.Count - 1 
    Dim FileName 
    FileName = WScript.Arguments(i) 
    Dim MyFile 
    On Error Resume Next 
    Set MyFile = fso.OpenTextFile(FileName, ForReading) 
    If Err.Number <> 0 Then 
        LogError "Error opening file " & FileName & ": " & Err.Description 
        Continue For 
    End If 
    Dim vbs 
    vbs = MyFile.ReadAll 
    If Err.Number <> 0 Then 
        LogError "Error reading file " & FileName & ": " & Err.Description 
        MyFile.Close 
        Continue For 
    End If 
    Defuscator(vbs) 
    MyFile.Close 
    LogStatus "Processed file " & FileName 
Next 
Set fso = Nothing