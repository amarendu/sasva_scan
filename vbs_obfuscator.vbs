' https://isvbscriptdead.com/vbs-obfuscator/ 
Option Explicit 
Function vbs_obfuscator(n) 
    On Error Resume Next 
    Dim r, k 
    r = Round(Rnd() * 10000) + 1 
    k = Round(Rnd() * 2) + 1 
    If Err.Number <> 0 Then 
        LogError "Error in generating random numbers: " & Err.Description 
        Exit Function 
    End If 
    Select Case k 
        Case 0 
            vbs_obfuscator = "CLng(&H" & Hex(r + n) & ")-" & r 
        Case 1 
            vbs_obfuscator = (n - r) & "+CLng(&H" & Hex(r) & ")" 
        Case Else 
            vbs_obfuscator = (n * r) & "/CLng(&H" & Hex(r) & ")" 
    End Select 
End Function 
Function Obfuscator(vbs) 
    On Error Resume Next 
    Dim length, s, i 
    length = Len(vbs) 
    s = "" 
    For i = 1 To length 
        Dim obfuscatedChar 
        obfuscatedChar = vbs_obfuscator(Asc(Mid(vbs, i))) 
        If Err.Number <> 0 Then 
            LogError "Error in obfuscating character at position " & i & ": " & Err.Description 
            Exit Function 
        End If 
        s = s & "chr(" & obfuscatedChar & ")&" 
    Next 
    s = s & "vbCrlf" 
    LogStatus "Obfuscation successful." 
    Obfuscator = "Execute " & s 
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
If WScript.Arguments.Count = 0 Then 
    WScript.Quit 
End If 
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
    Obfuscator(vbs) 
    MyFile.Close 
    LogStatus "Processed file " & FileName 
Next 
Set fso = Nothing