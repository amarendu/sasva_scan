using System; 
using System.IO; 
namespace AmarenduSasvaScan 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            if (args.Length == 0) 
            { 
                Console.WriteLine("Usage: dotnet run <VBScript source file(s)>"); 
                return; 
            } 
            Console.WriteLine("Welcome to the VBScript Obfuscation/De-obfuscation Tool"); 
            Console.WriteLine("Please select an obfuscation method:"); 
            Console.WriteLine("1. De-obfuscate"); 
            Console.WriteLine("2. Arithmetic Obfuscate"); 
            Console.WriteLine("3. Base64 Obfuscate"); 
            Console.WriteLine("4. ROT47 Obfuscate"); 
            Console.Write("Enter your choice (1-4): "); 
            string choice = Console.ReadLine(); 
            if (!IsValidChoice(choice)) 
            { 
                Console.WriteLine("Invalid choice. Please restart the application and select a valid option."); 
                return; 
            } 
            foreach (var fileName in args) 
            { 
                if (!File.Exists(fileName)) 
                { 
                    Console.WriteLine($"File not found: {fileName}"); 
                    continue; 
                } 
                try 
                { 
                    string vbsContent = File.ReadAllText(fileName); 
                    Console.WriteLine($"Processing file: {fileName}"); 
                    string processedContent = choice switch 
                    { 
                        "1" => Deobfuscator.Deobfuscate(vbsContent), 
                        "2" => ArithmeticObfuscator.Obfuscate(vbsContent), 
                        "3" => Base64Obfuscator.Obfuscate(vbsContent), 
                        "4" => Rot47Obfuscator.Obfuscate(vbsContent), 
                        _ => throw new ArgumentException("Invalid choice") 
                    }; 
                    Console.WriteLine("Processed Content:"); 
                    Console.WriteLine(processedContent); 
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine($"Error processing file {fileName}: {ex.Message}"); 
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                } 
            } 
        } 
        private static bool IsValidChoice(string choice) 
        { 
            return choice == "1" || choice == "2" || choice == "3" || choice == "4"; 
        } 
    } 
}