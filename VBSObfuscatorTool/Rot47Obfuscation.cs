using System; 
namespace VBSObfuscatorTool 
{ 
    public static class Rot47Obfuscation 
    { 
        public static string Obfuscate(string vbsContent) 
        { 
            try 
            { 
                Console.WriteLine("Starting ROT47 obfuscation process..."); 
                string obfuscatedContent = ApplyRot47(vbsContent); 
                string obfuscationScript = GenerateObfuscationScript(obfuscatedContent); 
                Console.WriteLine("ROT47 obfuscation process completed successfully."); 
                return obfuscationScript; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during ROT47 obfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
        private static string ApplyRot47(string input) 
        { 
            char[] result = new char[input.Length]; 
            for (int i = 0; i < input.Length; i++) 
            { 
                char c = input[i]; 
                if (c >= 33 && c <= 126) 
                { 
                    result[i] = (char)(33 + ((c + 14) % 94)); 
                } 
                else 
                { 
                    result[i] = c; 
                } 
            } 
            return new string(result); 
        } 
        private static string GenerateObfuscationScript(string obfuscatedContent) 
        { 
            string functionL = "Function l(str):Dim i,j,k,r:j=Len(str):r=\"\":For i=1 to j:k=Asc(Mid(str,i,1)):If k>=33 And k<=126 Then:r=r&Chr(33+((k+14)Mod 94)):Else:r=r&Chr(k):End If:Next:l=r:End Function"; 
            return $"{functionL}{Environment.NewLine}Execute l(\"{obfuscatedContent}\"){Environment.NewLine}"; 
        } 
        public static string Deobfuscate(string obfuscatedVbs) 
        { 
            try 
            { 
                Console.WriteLine("Starting ROT47 deobfuscation process..."); 
                int executeIndex = obfuscatedVbs.IndexOf("Execute", StringComparison.OrdinalIgnoreCase); 
                if (executeIndex == -1) 
                { 
                    throw new InvalidOperationException("Invalid obfuscated script format."); 
                } 
                string scriptBody = obfuscatedVbs.Substring(executeIndex + "Execute".Length); 
                string deobfuscatedContent = ApplyRot47(scriptBody); 
                Console.WriteLine("ROT47 deobfuscation process completed successfully."); 
                return deobfuscatedContent; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during ROT47 deobfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
    } 
}