' https://isvbscriptdead.com/vbs-obfuscator/ 
Option Explicit 
Function Defuscator(vbs) 
    Dim t 
    t = InStr(1, vbs, "Execute", 1) 
    t = Mid(vbs, t + Len("Execute")) 
    t = Eval(t) 
    Defuscator = t 
End Function 
Dim fso, i 
Const ForReading = 1 
Set fso = CreateObject("Scripting.FileSystemObject") 
' Log start of the de-obfuscation process 
WScript.Echo "Starting de-obfuscation process..." 
For i = 0 To WScript.Arguments.Count - 1 
    Dim FileName 
    FileName = WScript.Arguments(i) 
    Dim MyFile 
    Set MyFile = fso.OpenTextFile(FileName, ForReading) 
    Dim vbs 
    vbs = MyFile.ReadAll 
    WScript.Echo Defuscator(vbs) 
    MyFile.Close 
Next 
' Log end of the de-obfuscation process 
WScript.Echo "De-obfuscation process completed." 
Set fso = Nothing