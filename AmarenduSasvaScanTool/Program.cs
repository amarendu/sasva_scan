using System; 
using System.IO; 
using System.Diagnostics; 
using Serilog; 
using Serilog.Events; 
class Program 
{ 
    static void Main(string[] args) 
    { 
        // Initialize Serilog from configuration file 
        Log.Logger = new LoggerConfiguration() 
            .ReadFrom.Configuration(new ConfigurationBuilder() 
                .AddJsonFile("loggingConfig.json") 
                .Build()) 
            .CreateLogger(); 
        try 
        { 
            if (args.Length < 2) 
            { 
                Log.Error("Usage: <technique> <file-path1> [file-path2] ... [file-pathN]"); 
                Log.Error("Techniques: arithmetic, base64, rot47"); 
                Log.Error("Example: dotnet run arithmetic script1.vbs script2.vbs"); 
                Log.Error("Please provide the obfuscation technique and at least one VBScript file."); 
                return; 
            } 
            string technique = args[0].ToLower(); 
            for (int i = 1; i < args.Length; i++) 
            { 
                string filePath = args[i]; 
                if (!File.Exists(filePath)) 
                { 
                    Log.Error("File not found - {FilePath}", filePath); 
                    continue; 
                } 
                string vbsCode; 
                try 
                { 
                    vbsCode = File.ReadAllText(filePath); 
                } 
                catch (Exception ex) 
                { 
                    Log.Error(ex, "Error reading file {FilePath}", filePath); 
                    continue; 
                } 
                string obfuscatedCode = string.Empty; 
                try 
                { 
                    switch (technique) 
                    { 
                        case "arithmetic": 
                            Log.Information("Starting arithmetic obfuscation for {FilePath}", filePath); 
                            obfuscatedCode = ArithmeticObfuscator.Obfuscate(vbsCode); 
                            Log.Information("Arithmetic obfuscation completed successfully for {FilePath}", filePath); 
                            break; 
                        case "base64": 
                            Log.Information("Starting Base64 obfuscation for {FilePath}", filePath); 
                            obfuscatedCode = Base64Obfuscator.Obfuscate(vbsCode); 
                            Log.Information("Base64 obfuscation completed successfully for {FilePath}", filePath); 
                            break; 
                        case "rot47": 
                            Log.Information("Starting ROT47 obfuscation for {FilePath}", filePath); 
                            obfuscatedCode = Rot47Obfuscator.Obfuscate(vbsCode); 
                            Log.Information("ROT47 obfuscation completed successfully for {FilePath}", filePath); 
                            break; 
                        default: 
                            Log.Error("Unknown technique. Please use one of the following: arithmetic, base64, rot47."); 
                            continue; 
                    } 
                    // Execute the obfuscated script 
                    ExecuteObfuscatedScript(obfuscatedCode); 
                } 
                catch (Exception ex) 
                { 
                    Log.Error(ex, "Error during obfuscation for {FilePath}", filePath); 
                    continue; 
                } 
                Log.Information("Obfuscated code for {FilePath}:\n{ObfuscatedCode}\n", filePath, obfuscatedCode); 
            } 
            Log.Information("Obfuscation process completed for all provided files."); 
        } 
        catch (Exception ex) 
        { 
            Log.Fatal(ex, "Application terminated unexpectedly"); 
        } 
        finally 
        { 
            Log.CloseAndFlush(); 
        } 
    } 
    static void ExecuteObfuscatedScript(string obfuscatedCode) 
    { 
        try 
        { 
            Log.Information("Executing obfuscated script..."); 
            // Create a temporary file to store the obfuscated script 
            string tempFileName = Path.GetTempFileName() + ".vbs"; 
            File.WriteAllText(tempFileName, obfuscatedCode); 
            // Start the Windows Script Host process 
            ProcessStartInfo processInfo = new ProcessStartInfo("cscript.exe") 
            { 
                Arguments = $"//NoLogo \"{tempFileName}\"", 
                RedirectStandardOutput = true, 
                UseShellExecute = false, 
                CreateNoWindow = true 
            }; 
            using (Process process = Process.Start(processInfo)) 
            { 
                using (StreamReader reader = process.StandardOutput) 
                { 
                    string result = reader.ReadToEnd(); 
                    Log.Information("Script Output:\n{Result}", result); 
                } 
            } 
            // Clean up the temporary file 
            File.Delete(tempFileName); 
            Log.Information("Execution of obfuscated script completed."); 
        } 
        catch (Exception ex) 
        { 
            Log.Error(ex, "Error executing obfuscated script"); 
        } 
    } 
}