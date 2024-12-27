' logger.vbs 
Option Explicit 
Dim logFilePath 
logFilePath = CreateObject("Scripting.FileSystemObject").GetParentFolderName(WScript.ScriptFullName) & "\defuscator.log" 
Sub LogMessage(message) 
    Dim fso, logFile 
    On Error Resume Next 
    Set fso = CreateObject("Scripting.FileSystemObject") 
    If Err.Number <> 0 Then 
        WScript.Echo "Error creating FileSystemObject: " & Err.Description 
        Exit Sub 
    End If 
    On Error GoTo 0 
    On Error Resume Next 
    Set logFile = fso.OpenTextFile(logFilePath, 8, True) 
    If Err.Number <> 0 Then 
        WScript.Echo "Error opening log file: " & Err.Description 
        Set fso = Nothing 
        Exit Sub 
    End If 
    On Error GoTo 0 
    logFile.WriteLine Now & " - " & message 
    logFile.Close 
    Set logFile = Nothing 
    Set fso = Nothing 
End Sub