using System; 
using System.Text; 
using System.Threading.Tasks; 
namespace VbScriptObfuscator.Services 
{ 
    public class Base64Obfuscator 
    { 
        public async Task<string> ObfuscateAsync(string input) 
        { 
            try 
            { 
                return await Task.Run(() => 
                { 
                    // Convert the input string to UTF-8 bytes 
                    var utf8Bytes = Encoding.UTF8.GetBytes(input); 
                    // Convert the UTF-8 bytes to a Base64 string 
                    var base64String = Convert.ToBase64String(utf8Bytes); 
                    // VBScript functions for Base64 decoding 
                    var f1 = "Function l(a): With CreateObject(\"Msxml2.DOMDocument\").CreateElement(\"aux\"): .DataType = \"bin.base64\": .Text = a: l = r(.NodeTypedValue): End With: End Function"; 
                    var f2 = "Function r(b): With CreateObject(\"ADODB.Stream\"): .Type = 1: .Open: .Write b: .Position = 0: .Type = 2: .CharSet = \"utf-8\": r = .ReadText: .Close:  End With: End function"; 
                    // Return the obfuscated VBScript 
                    return $"{f1}\n{f2}\nExecute l(\"{base64String}\")\n"; 
                }); 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Console.Error.WriteLine($"Error in Base64Obfuscator.ObfuscateAsync: {ex}"); 
                throw; 
            } 
        } 
    } 
}