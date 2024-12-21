using System; 
using System.IO; 
using VBSObfuscatorTool; 
namespace VBSObfuscatorTool 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            if (args.Length == 0) 
            { 
                Console.WriteLine("Missing parameter(s): VBScript source file(s)"); 
                return; 
            } 
            string obfuscationType = "arithmetic"; // Default obfuscation type 
            if (args.Length > 1) 
            { 
                obfuscationType = args[1].ToLower(); 
            } 
            foreach (var filePath in args) 
            { 
                try 
                { 
                    if (File.Exists(filePath)) 
                    { 
                        string vbsContent = File.ReadAllText(filePath); 
                        Console.WriteLine($"Processing file: {filePath}"); 
                        string obfuscatedContent; 
                        switch (obfuscationType) 
                        { 
                            case "base64": 
                                obfuscatedContent = Base64Obfuscation.Obfuscate(vbsContent); 
                                break; 
                            case "rot47": 
                                obfuscatedContent = Rot47Obfuscation.Obfuscate(vbsContent); 
                                break; 
                            default: 
                                obfuscatedContent = ArithmeticObfuscation.Obfuscate(vbsContent); 
                                break; 
                        } 
                        Console.WriteLine("Obfuscated Content:"); 
                        Console.WriteLine(obfuscatedContent); 
                        // Deobfuscation example 
                        string deobfuscatedContent; 
                        switch (obfuscationType) 
                        { 
                            case "base64": 
                                deobfuscatedContent = Base64Obfuscation.Deobfuscate(obfuscatedContent); 
                                break; 
                            case "rot47": 
                                deobfuscatedContent = Rot47Obfuscation.Deobfuscate(obfuscatedContent); 
                                break; 
                            default: 
                                deobfuscatedContent = ArithmeticObfuscation.Deobfuscate(obfuscatedContent); 
                                break; 
                        } 
                        Console.WriteLine("Deobfuscated Content:"); 
                        Console.WriteLine(deobfuscatedContent); 
                    } 
                    else 
                    { 
                        Console.WriteLine($"File not found: {filePath}"); 
                    } 
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine($"Error processing file {filePath}: {ex.Message}"); 
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                } 
            } 
        } 
    } 
}