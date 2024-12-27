Option Explicit 
' Logger function to log messages to a file based on log level 
Sub Logger(message, level) 
    Dim logFile, fso, logStream, currentLevel 
    logFile = "defuscator_log.txt" 
    currentLevel = GetLogLevel() 
    If ShouldLog(level, currentLevel) Then 
        Set fso = CreateObject("Scripting.FileSystemObject") 
        Set logStream = fso.OpenTextFile(logFile, 8, True) ' 8 for appending 
        logStream.WriteLine Now & " - " & level & " - " & message 
        logStream.Close 
        Set logStream = Nothing 
        Set fso = Nothing 
    End If 
End Sub 
' Function to determine if a message should be logged based on the current level 
Function ShouldLog(messageLevel, currentLevel) 
    Dim levels 
    levels = Array("error", "info", "debug") 
    ShouldLog = False 
    If ArrayContains(levels, messageLevel) And ArrayContains(levels, currentLevel) Then 
        If ArrayIndex(levels, messageLevel) <= ArrayIndex(levels, currentLevel) Then 
            ShouldLog = True 
        End If 
    End If 
End Function 
' Function to get the current logging level from command line arguments 
Function GetLogLevel() 
    Dim logLevel, i 
    logLevel = "info" ' default level 
    For i = 0 To WScript.Arguments.Count - 1 
        If InStr(1, WScript.Arguments(i), "--log-level=") = 1 Then 
            logLevel = Mid(WScript.Arguments(i), 13) 
            Exit For 
        End If 
    Next 
    GetLogLevel = logLevel 
End Function 
' Utility function to check if an array contains a value 
Function ArrayContains(arr, value) 
    Dim i 
    ArrayContains = False 
    For i = LBound(arr) To UBound(arr) 
        If arr(i) = value Then 
            ArrayContains = True 
            Exit Function 
        End If 
    Next 
End Function 
' Utility function to find the index of a value in an array 
Function ArrayIndex(arr, value) 
    Dim i 
    ArrayIndex = -1 
    For i = LBound(arr) To UBound(arr) 
        If arr(i) = value Then 
            ArrayIndex = i 
            Exit Function 
        End If 
    Next 
End Function 
Function Defuscator(vbs) 
    Dim t, evalStart 
    evalStart = InStr(1, vbs, "Execute", 1) 
    If evalStart > 0 Then 
        t = Mid(vbs, evalStart + Len("Execute")) 
        On Error Resume Next 
        t = Eval(t) 
        If Err.Number <> 0 Then 
            Logger "Error during evaluation: " & Err.Description, "error" 
            Err.Clear 
        End If 
        On Error GoTo 0 
        Defuscator = t 
    Else 
        Logger "No executable code found.", "info" 
        Defuscator = "Error: No executable code found." 
    End If 
End Function 
Dim fso, i 
Const ForReading = 1 
Set fso = CreateObject("Scripting.FileSystemObject") 
Logger "Script execution started.", "info" 
For i = 0 To WScript.Arguments.Count - 1 
    If Left(WScript.Arguments(i), 12) = "--log-level=" Then 
        Continue For 
    End If 
    Dim FileName 
    FileName = WScript.Arguments(i) 
    On Error Resume Next 
    Dim MyFile 
    Set MyFile = fso.OpenTextFile(FileName, ForReading) 
    If Err.Number <> 0 Then 
        Logger "Error opening file: " & FileName & " - " & Err.Description, "error" 
        Err.Clear 
        On Error GoTo 0 
        Continue For 
    End If 
    Dim vbs 
    vbs = MyFile.ReadAll 
    Logger "Processing file: " & FileName, "info" 
    Dim result 
    result = Defuscator(vbs) 
    If Left(result, 5) = "Error" Then 
        Logger "Error processing file: " & FileName & " - " & result, "error" 
    Else 
        WScript.Echo result 
    End If 
    MyFile.Close 
    Logger "Finished processing file: " & FileName, "info" 
    On Error GoTo 0 
Next 
Logger "Script execution ended.", "info" 
Set fso = Nothing