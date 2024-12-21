using System; 
using System.Text; 
namespace VBSObfuscatorTool 
{ 
    public static class Base64Obfuscation 
    { 
        public static string Obfuscate(string vbsContent) 
        { 
            try 
            { 
                Console.WriteLine("Starting Base64 obfuscation process..."); 
                string base64Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(vbsContent)); 
                string obfuscatedContent = GenerateObfuscationScript(base64Content); 
                Console.WriteLine("Base64 obfuscation process completed successfully."); 
                return obfuscatedContent; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during Base64 obfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
        private static string GenerateObfuscationScript(string base64Content) 
        { 
            string functionL = "Function l(a): With CreateObject(\"Msxml2.DOMDocument\").CreateElement(\"aux\"): .DataType = \"bin.base64\": .Text = a: l = r(.NodeTypedValue): End With: End Function"; 
            string functionR = "Function r(b): With CreateObject(\"ADODB.Stream\"): .Type = 1: .Open: .Write b: .Position = 0: .Type = 2: .CharSet = \"utf-8\": r = .ReadText: .Close:  End With: End function"; 
            return $"{functionL}{Environment.NewLine}{functionR}{Environment.NewLine}Execute l(\"{base64Content}\"){Environment.NewLine}"; 
        } 
        public static string Deobfuscate(string obfuscatedVbs) 
        { 
            try 
            { 
                Console.WriteLine("Starting Base64 deobfuscation process..."); 
                int executeIndex = obfuscatedVbs.IndexOf("Execute", StringComparison.OrdinalIgnoreCase); 
                if (executeIndex == -1) 
                { 
                    throw new InvalidOperationException("Invalid obfuscated script format."); 
                } 
                string base64Content = ExtractBase64Content(obfuscatedVbs, executeIndex); 
                string deobfuscatedContent = Encoding.UTF8.GetString(Convert.FromBase64String(base64Content)); 
                Console.WriteLine("Base64 deobfuscation process completed successfully."); 
                return deobfuscatedContent; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during Base64 deobfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
        private static string ExtractBase64Content(string obfuscatedVbs, int executeIndex) 
        { 
            int startIndex = obfuscatedVbs.IndexOf("\"", executeIndex) + 1; 
            int endIndex = obfuscatedVbs.IndexOf("\"", startIndex); 
            if (startIndex == -1 || endIndex == -1) 
            { 
                throw new InvalidOperationException("Base64 content not found in the script."); 
            } 
            return obfuscatedVbs.Substring(startIndex, endIndex - startIndex); 
        } 
    } 
}