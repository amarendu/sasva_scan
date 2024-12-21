using System; 
using System.Text; 
using System.Threading.Tasks; 
namespace VbScriptObfuscator.Services 
{ 
    public class ArithmeticObfuscator 
    { 
        private static readonly Random Random = new Random(); 
        public async Task<string> ObfuscateAsync(string input) 
        { 
            try 
            { 
                return await Task.Run(() => 
                { 
                    var sb = new StringBuilder(); 
                    foreach (var ch in input) 
                    { 
                        var n = (int)ch; 
                        var r = Random.Next(1, 10001); 
                        var k = Random.Next(0, 3); 
                        string obfuscatedChar = k switch 
                        { 
                            0 => $"CLng(&H{(r + n):X})-{r}", 
                            1 => $"{n - r}+CLng(&H{r:X})", 
                            _ => $"{n * r}/CLng(&H{r:X})" 
                        }; 
                        sb.Append($"chr({obfuscatedChar})&"); 
                    } 
                    sb.Append("vbCrlf"); 
                    return $"Execute {sb.ToString()}"; 
                }); 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Console.Error.WriteLine($"Error in ArithmeticObfuscator.ObfuscateAsync: {ex}"); 
                throw; 
            } 
        } 
    } 
}