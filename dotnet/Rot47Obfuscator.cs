using System; 
using System.Text; 
namespace AmarenduSasvaScan 
{ 
    public static class Rot47Obfuscator 
    { 
        public static string Obfuscate(string vbsContent) 
        { 
            try 
            { 
                Console.WriteLine("Starting ROT47 obfuscation..."); 
                StringBuilder obfuscatedBuilder = new StringBuilder("Execute "); 
                string obfuscatedContent = Rot47(vbsContent); 
                string function = "Function l(str):Dim i,j,k,r:j=Len(str):r=\"\":For i=1 to j:k=Asc(Mid(str,i,1)):If k>=33 And k<=126 Then:r=r&Chr(33+((k+14)Mod 94)):Else:r=r&Chr(k):End If:Next:l=r:End Function"; 
                obfuscatedBuilder.Append(function).Append("\nExecute l(\"").Append(obfuscatedContent).Append("\")\n"); 
                Console.WriteLine("ROT47 obfuscation completed successfully."); 
                return obfuscatedBuilder.ToString(); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during ROT47 obfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
        private static string Rot47(string input) 
        { 
            StringBuilder result = new StringBuilder(input.Length); 
            foreach (char c in input) 
            { 
                if (c >= 33 && c <= 126) 
                { 
                    result.Append((char)(33 + ((c + 14) % 94))); 
                } 
                else 
                { 
                    result.Append(c); 
                } 
            } 
            return result.ToString(); 
        } 
    } 
} 