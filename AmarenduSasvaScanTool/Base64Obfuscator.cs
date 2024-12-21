using System; 
using System.Text; 
public static class Base64Obfuscator 
{ 
    public static string Obfuscate(string input) 
    { 
        try 
        { 
            Console.WriteLine("Starting Base64 obfuscation."); 
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(input)); 
            string functionWrap = @" 
Function l(a): With CreateObject(""Msxml2.DOMDocument"").CreateElement(""aux""): .DataType = ""bin.base64"": .Text = a: l = r(.NodeTypedValue): End With: End Function 
Function r(b): With CreateObject(""ADODB.Stream""): .Type = 1: .Open: .Write b: .Position = 0: .Type = 2: .CharSet = ""utf-8"": r = .ReadText: .Close:  End With: End function 
"; 
            Console.WriteLine("Base64 obfuscation completed successfully."); 
            return functionWrap + $"Execute l(\"{base64String}\")"; 
        } 
        catch (Exception ex) 
        { 
            Console.WriteLine("Error during Base64 obfuscation: " + ex.Message); 
            Console.WriteLine("Stack Trace: " + ex.StackTrace); 
            throw; 
        } 
    } 
}