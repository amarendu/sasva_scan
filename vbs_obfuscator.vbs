' Enhanced Arithmetic Obfuscator Script 
Option Explicit 
' Function to apply random arithmetic transformations to a character's ASCII value 
' This function takes an ASCII value of a character and applies a random arithmetic transformation 
' to obfuscate it. The transformation is randomly chosen from three possible cases. 
Function vbs_obfuscator(n) 
    Dim r, k 
    ' Generate a random number to use in the transformation 
    r = Round(Rnd() * 10000) + 1 
    ' Randomly select a transformation case 
    k = Round(Rnd() * 2) + 1 
    Select Case k 
        Case 0 
            ' Case 0: Add the random number to the ASCII value and convert to hexadecimal 
            vbs_obfuscator = "CLng(&H" & Hex(r + n) & ")-" & r 
        Case 1 
            ' Case 1: Subtract the random number from the ASCII value and convert to hexadecimal 
            vbs_obfuscator = (n - r) & "+CLng(&H" & Hex(r) & ")" 
        Case Else 
            ' Default case: Multiply the ASCII value by the random number and divide by the hexadecimal 
            vbs_obfuscator = (n * r) & "/CLng(&H" & Hex(r) & ")" 
    End Select 
End Function 
' Function to obfuscate VBScript code using arithmetic transformations 
' This function takes a VBScript code as input and obfuscates it by transforming each character 
' using the vbs_obfuscator function. The result is a string of obfuscated code that can be executed. 
Function Obfuscator(vbs) 
    Dim length, s, i 
    ' Get the length of the input script 
    length = Len(vbs) 
    s = "" 
    ' Transform each character in the script 
    For i = 1 To length 
        s = s & "chr(" & vbs_obfuscator(Asc(Mid(vbs, i, 1))) & ")&" 
    Next 
    ' Remove the trailing '&' from the final string 
    s = Left(s, Len(s) - 1) 
    ' Return the obfuscated script with the "Execute" command 
    Obfuscator = "Execute " & s 
End Function 
' Check if any files are provided as arguments 
If WScript.Arguments.Count = 0 Then 
    WScript.Echo "Missing parameter(s): VBScript source file(s)" 
    WScript.Quit 1 
End If 
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
    ' Output the obfuscated code 
    WScript.Echo Obfuscator(vbs) 
    MyFile.Close 
Next 
Set fso = Nothing