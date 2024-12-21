using System; 
using System.Text; 
using System.Threading.Tasks; 
namespace VbScriptObfuscator.Services 
{ 
    public class Rot47Obfuscator 
    { 
        public async Task<string> ObfuscateAsync(string input) 
        { 
            try 
            { 
                return await Task.Run(() => 
                { 
                    var sb = new StringBuilder(); 
                    foreach (var ch in input) 
                    { 
                        if (ch >= 33 && ch <= 126) 
                        { 
                            sb.Append((char)(33 + ((ch + 14) % 94))); 
                        } 
                        else 
                        { 
                            sb.Append(ch); 
                        } 
                    } 
                    var f = "Function l(str):Dim i,j,k,r:j=Len(str):r=\"\":For i=1 to j:k=Asc(Mid(str,i,1)):If k>=33 And k<=126 Then:r=r&Chr(33+((k+14)Mod 94)):Else:r=r&Chr(k):End If:Next:l=r:End Function"; 
                    return $"{f}\nExecute l(\"{sb.ToString()}\")\n"; 
                }); 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Console.Error.WriteLine($"Error in Rot47Obfuscator.ObfuscateAsync: {ex}"); 
                throw; 
            } 
        } 
    } 
}