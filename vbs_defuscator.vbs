' https://isvbscriptdead.com/vbs-obfuscator/ 
Option Explicit 
' Log levels 
Const LOG_INFO = "INFO" 
Const LOG_ERROR = "ERROR" 
' Default log level 
Dim logLevel 
logLevel = LOG_INFO ' Set the default log level to INFO 
' Flag to control logging 
Dim isLoggingEnabled 
isLoggingEnabled = True ' Default to logging enabled 
' Function to log messages based on the current log level and logging flag 
Sub LogMessage(level, message) 
    If isLoggingEnabled Then 
        If (level = LOG_ERROR) Or (level = LOG_INFO And logLevel = LOG_INFO) Then 
            WScript.Echo "[" & level & "] " & message 
        End If 
    End If 
End Sub 
Function Defuscator(vbs) 
    Dim t, executeStart, executeEnd, codeToExecute 
    executeStart = InStr(1, vbs, "Execute", 1) 
    If executeStart = 0 Then 
        LogMessage LOG_ERROR, "No 'Execute' statement found in the script." 
        Exit Function 
    End If 
    ' Extract the code following the 'Execute' statement 
    executeEnd = InStr(executeStart, vbs, vbCrLf) ' Find end of line 
    If executeEnd = 0 Then 
        executeEnd = Len(vbs) + 1 ' If no newline, assume end of script 
    End If 
    codeToExecute = Trim(Mid(vbs, executeStart + Len("Execute"), executeEnd - executeStart - Len("Execute"))) 
    LogMessage LOG_INFO, "Code to execute: " & codeToExecute 
    On Error Resume Next 
    t = Eval(codeToExecute) 
    If Err.Number <> 0 Then 
        LogMessage LOG_ERROR, "Error during de-obfuscation: " & Err.Description 
        Err.Clear 
    Else 
        Defuscator = t 
    End If 
    On Error Goto 0 
End Function 
Dim fso, i 
Const ForReading = 1 
Set fso = CreateObject("Scripting.FileSystemObject") 
' Check command-line arguments for logging option 
Dim logArgIndex 
logArgIndex = -1 
For i = 0 To WScript.Arguments.Count - 1 
    If WScript.Arguments(i) = "--no-log" Then 
        isLoggingEnabled = False 
        logArgIndex = i 
        Exit For 
    ElseIf WScript.Arguments(i) = "--log" Then 
        isLoggingEnabled = True 
        logArgIndex = i 
        Exit For 
    End If 
Next 
' Adjust arguments count if log argument is present 
If logArgIndex <> -1 Then 
    WScript.Arguments.Remove logArgIndex 
End If 
LogMessage LOG_INFO, "De-obfuscation process started." 
For i = 0 To WScript.Arguments.Count - 1 
    Dim FileName, MyFile, vbs, FileSize 
    FileName = WScript.Arguments(i) 
    ' Log the input file name 
    LogMessage LOG_INFO, "Processing file: " & FileName 
    ' Open the file and log its size 
    Set MyFile = fso.OpenTextFile(FileName, ForReading) 
    vbs = MyFile.ReadAll 
    FileSize = Len(vbs) 
    LogMessage LOG_INFO, "File size: " & FileSize & " bytes" 
    ' Perform de-obfuscation 
    Dim result 
    result = Defuscator(vbs) 
    If result <> "" Then 
        WScript.Echo result 
    End If 
    MyFile.Close 
Next 
LogMessage LOG_INFO, "De-obfuscation process completed." 
Set fso = Nothing