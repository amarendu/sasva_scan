using System; 
using System.Text; 
namespace VBSObfuscatorTool 
{ 
    public static class Obfuscation 
    { 
        public static string Obfuscate(string vbsContent) 
        { 
            try 
            { 
                Console.WriteLine("Starting obfuscation process..."); 
                StringBuilder obfuscatedContent = new StringBuilder(); 
                Random random = new Random(); 
                foreach (char c in vbsContent) 
                { 
                    int n = (int)c; 
                    int r = random.Next(1, 10001); 
                    int k = random.Next(0, 3); 
                    string obfuscatedChar; 
                    switch (k) 
                    { 
                        case 0: 
                            obfuscatedChar = $"CLng(&H{(r + n):X})-{r}"; 
                            break; 
                        case 1: 
                            obfuscatedChar = $"{(n - r)}+CLng(&H{r:X})"; 
                            break; 
                        default: 
                            obfuscatedChar = $"{(n * r)}/CLng(&H{r:X})"; 
                            break; 
                    } 
                    obfuscatedContent.Append($"chr({obfuscatedChar})&"); 
                } 
                obfuscatedContent.Append("vbCrlf"); 
                string result = $"Execute {obfuscatedContent.ToString()}"; 
                Console.WriteLine("Obfuscation process completed successfully."); 
                return result; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during obfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
        public static string Deobfuscate(string obfuscatedVbs) 
        { 
            try 
            { 
                Console.WriteLine("Starting deobfuscation process..."); 
                int executeIndex = obfuscatedVbs.IndexOf("Execute", StringComparison.OrdinalIgnoreCase); 
                if (executeIndex == -1) 
                { 
                    throw new InvalidOperationException("Invalid obfuscated script format."); 
                } 
                string scriptBody = obfuscatedVbs.Substring(executeIndex + "Execute".Length); 
                // Simulate deobfuscation logic 
                string deobfuscatedContent = scriptBody; // Placeholder for actual deobfuscation logic 
                Console.WriteLine("Deobfuscation process completed successfully."); 
                return deobfuscatedContent; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during deobfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
    } 
} 