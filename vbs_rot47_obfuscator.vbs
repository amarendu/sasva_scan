' Enhanced ROT47 Obfuscator Script 
Option Explicit 
' Function to apply ROT47 cipher to a string 
' This function takes a string as input and applies the ROT47 cipher to each character. 
' Characters in the ASCII range 33 to 126 are shifted by 47 positions, wrapping around if necessary. 
Function Rot47(str) 
    Dim i, j, k, r 
    j = Len(str) 
    r = "" 
    ' Transform each character using ROT47 
    For i = 1 to j 
        k = Asc(Mid(str, i, 1)) 
        If k >= 33 And k <= 126 Then 
            r = r & Chr(33 + ((k + 14) Mod 94)) 
        Else 
            r = r & Chr(k) 
        End If 
    Next 
    Rot47 = r 
End Function 
' Function to obfuscate VBScript code using ROT47 
' This function takes VBScript code as input, applies the ROT47 cipher to obfuscate it, 
' and wraps it in a function that decodes and executes the script. 
Function Obfuscator(vbs) 
    Dim length, s, i, F 
    ' Define function to decode ROT47 
    F = "Function l(str):Dim i,j,k,r:j=Len(str):r=" & Chr(34) & Chr(34) & ":For i=1 to j:k=Asc(Mid(str,i,1)):If k>=33 And k<=126 Then:r=r&Chr(33+((k+14)Mod 94)):Else:r=r&Chr(k):End If:Next:l=r:End Function" 
    length = Len(vbs) 
    s = "" 
    ' Transform each character in the script 
    For i = 1 To length 
        s = s & Rot47(Mid(vbs, i, 1)) 
    Next 
    ' Return the obfuscated script with the "Execute" command 
    Obfuscator = F & vbCrLf & "Execute l(" & Chr(34) & (s)& Chr(34) &")" & vbCrLf 
End Function 
' Check if any files are provided as arguments 
If WScript.Arguments.Count = 0 Then 
    WScript.Echo "Missing parameter(s): VBScript source file(s)" 
    WScript.Quit 
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