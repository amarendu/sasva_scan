' https://isvbscriptdead.com/vbs-obfuscator/ 
Option Explicit 
Function Rot47(str) 
    On Error Resume Next 
    Dim i, j, k, r 
    j = Len(str) 
    r = "" 
    For i = 1 to j 
        k = Asc(Mid(str, i, 1)) 
        If k >= 33 And k <= 126 Then 
            r = r & Chr(33 + ((k + 14) Mod 94)) 
        Else 
            r = r & Chr(k) 
        End If 
        If Err.Number <> 0 Then 
            LogError "Error in ROT47 encoding for character at position " & i & ": " & Err.Description 
            Exit Function 
        End If 
    Next 
    Rot47 = r 
End Function 
Function Obfuscator(vbs) 
    On Error Resume Next 
    Dim length, s, i, F 
    F = "Function l(str):Dim i,j,k,r:j=Len(str):r=" & Chr(34) & Chr(34) & ":For i=1 to j:k=Asc(Mid(str,i,1)):If k>=33 And k<=126 Then:r=r&Chr(33+((k+14)Mod 94)):Else:r=r&Chr(k):End If:Next:l=r:End Function" 
    length = Len(vbs) 
    s = "" 
    For i = 1 To length 
        s = s & Rot47(Mid(vbs, i, 1)) 
        If Err.Number <> 0 Then 
            LogError "Error in ROT47 obfuscation at position " & i & ": " & Err.Description 
            Exit Function 
        End If 
    Next 
    LogStatus "ROT47 obfuscation successful." 
    Obfuscator = F & vbCrlf & "Execute l(" & Chr(34) & (s)& Chr(34) &")" & vbCrLf 
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