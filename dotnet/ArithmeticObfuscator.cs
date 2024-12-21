using System; 
using System.Text; 
namespace AmarenduSasvaScan 
{ 
    public static class ArithmeticObfuscator 
    { 
        private static readonly Random RandomGenerator = new Random(); 
        public static string Obfuscate(string vbsContent) 
        { 
            var obfuscatedBuilder = new StringBuilder("Execute "); 
            try 
            { 
                foreach (char character in vbsContent) 
                { 
                    int asciiValue = (int)character; 
                    string obfuscatedChar = ObfuscateCharacter(asciiValue); 
                    obfuscatedBuilder.Append($"chr({obfuscatedChar})&"); 
                } 
                obfuscatedBuilder.Append("vbCrlf"); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during obfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
            return obfuscatedBuilder.ToString(); 
        } 
        private static string ObfuscateCharacter(int asciiValue) 
        { 
            try 
            { 
                int r = RandomGenerator.Next(1, 10001); 
                int k = RandomGenerator.Next(0, 3); 
                return k switch 
                { 
                    0 => $"CLng(&H{(r + asciiValue):X})-{r}", 
                    1 => $"{asciiValue - r}+CLng(&H{r:X})", 
                    _ => $"{asciiValue * r}/CLng(&H{r:X})" 
                }; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error obfuscating character with ASCII value {asciiValue}: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
    } 
}