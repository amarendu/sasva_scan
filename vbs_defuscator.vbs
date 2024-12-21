' Enhanced Defuscator Script 
Option Explicit 
' Function to de-obfuscate VBScript code 
' This function takes an obfuscated VBScript as input, searches for the "Execute" keyword, 
' extracts the code following it, evaluates the extracted code, and returns the de-obfuscated code. 
Function Defuscator(vbs) 
    Dim t 
    ' Find the position of the "Execute" keyword 
    t = InStr(1, vbs, "Execute", 1) 
    If t = 0 Then 
        ' If "Execute" is not found, output an error message and terminate the script 
        WScript.Echo "Error: 'Execute' keyword not found in the script." 
        WScript.Quit 1 
    End If 
    ' Extract the code following the "Execute" keyword 
    t = Mid(vbs, t + Len("Execute")) 
    On Error Resume Next 
    ' Evaluate the extracted code 
    t = Eval(t) 
    If Err.Number <> 0 Then 
        ' If evaluation fails, output an error message with the description and terminate the script 
        WScript.Echo "Error: Failed to evaluate the script. " & Err.Description 
        WScript.Quit 1 
    End If 
    On Error GoTo 0 
    ' Return the de-obfuscated code 
    Defuscator = t 
End Function 
Dim fso, i 
Const ForReading = 1 
Set fso = CreateObject("Scripting.FileSystemObject") 
' Process each file passed as an argument 
For i = 0 To WScript.Arguments.Count - 1 
    Dim FileName 
    FileName = WScript.Arguments(i) 
    If Not fso.FileExists(FileName) Then 
        ' If the file does not exist, output an error message and terminate the script 
        WScript.Echo "Error: File '" & FileName & "' not found." 
        WScript.Quit 1 
    End If 
    Dim MyFile, vbs 
    On Error Resume Next 
    ' Open and read the file 
    Set MyFile = fso.OpenTextFile(FileName, ForReading) 
    If Err.Number <> 0 Then 
        ' If unable to open the file, output an error message with the description and terminate the script 
        WScript.Echo "Error: Unable to open file '" & FileName & "'. " & Err.Description 
        WScript.Quit 1 
    End If 
    vbs = MyFile.ReadAll 
    If Err.Number <> 0 Then 
        ' If unable to read the file, output an error message with the description and terminate the script 
        WScript.Echo "Error: Unable to read file '" & FileName & "'. " & Err.Description 
        WScript.Quit 1 
    End If 
    On Error GoTo 0 
    ' Output the de-obfuscated code 
    WScript.Echo Defuscator(vbs) 
    MyFile.Close 
Next 
Set fso = Nothing