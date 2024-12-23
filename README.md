# sasva_scan
Created a repo for sasva project
# Amarendu SASVA Scan - VBScript Obfuscation and De-obfuscation Tools 
## Overview 
This project is a suite of VBScript tools designed to manipulate and analyze VBScript code through obfuscation and de-obfuscation techniques. It aims to provide users with the ability to obfuscate VBScript code using various methods to protect the code from unauthorized access or modification. Additionally, the project includes a de-obfuscator tool that reverses the obfuscation process, making it easier to read and understand the original code. 
## Features 
1. **Obfuscation using Random Arithmetic Transformations** 
   - This tool applies random arithmetic transformations to the ASCII values of characters in VBScript code, generating obfuscated scripts that are difficult to interpret without de-obfuscation. 
2. **Base64 Encoding Obfuscation** 
   - This tool obfuscates VBScript code by converting it into a Base64 encoded string, which is then embedded in a function that decodes and executes the code. 
3. **ROT47 Cipher Obfuscation** 
   - This tool uses the ROT47 algorithm to obfuscate VBScript code, replacing characters with others 47 positions down the ASCII table. 
4. **De-obfuscation of VBScript Code** 
   - This tool reverses the obfuscation process, making the VBScript code readable and understandable again. 
## Usage 
### Prerequisites 
- Ensure you have Windows Scripting Host installed on your system to execute VBScript files. 
### Command-Line Usage 
Each tool in this suite is executed via the command line. Here are the instructions for using each tool: 
#### 1. Obfuscation using Random Arithmetic Transformations 
```bash 
cscript vbs_obfuscator.vbs <path_to_vbscript_file> 
``` 
- Replace `<path_to_vbscript_file>` with the path to your VBScript file that you want to obfuscate. 
#### 2. Base64 Encoding Obfuscation 
```bash 
cscript vbs_obfuscator_base64.vbs <path_to_vbscript_file> 
``` 
- Replace `<path_to_vbscript_file>` with the path to your VBScript file that you want to obfuscate. 
#### 3. ROT47 Cipher Obfuscation 
```bash 
cscript vbs_rot47_obfuscator.vbs <path_to_vbscript_file> 
``` 
- Replace `<path_to_vbscript_file>` with the path to your VBScript file that you want to obfuscate. 
#### 4. De-obfuscation 
```bash 
cscript vbs_defuscator.vbs <path_to_obfuscated_vbscript_file> 
``` 
- Replace `<path_to_obfuscated_vbscript_file>` with the path to your obfuscated VBScript file that you want to de-obfuscate. 
## Examples 
### Example of Random Arithmetic Transformation 
Input: 
```vbscript 
msgbox "Hello, World!" 
``` 
Command: 
```bash 
cscript vbs_obfuscator.vbs example.vbs 
``` 
Output: 
```vbscript 
Execute chr(CLng(&H3E8)-1000)&chr(CLng(&H3E9)-999)&... 
``` 
### Example of Base64 Encoding 
Input: 
```vbscript 
msgbox "Hello, World!" 
``` 
Command: 
```bash 
cscript vbs_obfuscator_base64.vbs example.vbs 
``` 
Output: 
```vbscript 
Function l(a): ... Execute l("SGVsbG8sIFdvcmxkIQ==") 
``` 
### Example of ROT47 Cipher 
Input: 
```vbscript 
msgbox "Hello, World!" 
``` 
Command: 
```bash 
cscript vbs_rot47_obfuscator.vbs example.vbs 
``` 
Output: 
```vbscript 
Function l(str): ... Execute l("w6==@FC:D") 
``` 
## Error Handling 
- Ensure that the input file paths are correct and accessible. 
- The tools will output error messages if they encounter issues with file access or invalid input. 
## Contribution 
Feel free to contribute to this project by submitting issues or pull requests. Ensure your code follows the project's coding standards and includes appropriate documentation. 