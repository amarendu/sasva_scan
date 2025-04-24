using System; 
using System.Text; 
namespace AmarenduSasvaScan 
{ 
    public static class Base64Obfuscator 
    { 
        public static string Obfuscate(string vbsContent) 
        { 
            try 
            { 
                Console.WriteLine("Starting Base64 obfuscation..."); 
                string base64Encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(vbsContent)); 
                string function1 = "Function l(a): With CreateObject(\"Msxml2.DOMDocument\").CreateElement(\"aux\"): .DataType = \"bin.base64\": .Text = a: l = r(.NodeTypedValue): End With: End Function"; 
                string function2 = "Function r(b): With CreateObject(\"ADODB.Stream\"): .Type = 1: .Open: .Write b: .Position = 0: .Type = 2: .CharSet = \"utf-8\": r = .ReadText: .Close:  End With: End function"; 
                string obfuscatedContent = $"{function1}\n{function2}\nExecute l(\"{base64Encoded}\")\n"; 
                Console.WriteLine("Base64 obfuscation completed successfully."); 
                return obfuscatedContent; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during Base64 obfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
    } 
} 