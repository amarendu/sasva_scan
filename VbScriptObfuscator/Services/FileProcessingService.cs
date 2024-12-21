using System; 
using System.Threading.Tasks; 
namespace VbScriptObfuscator.Services 
{ 
    public class FileProcessingService : IFileProcessingService 
    { 
        private readonly ArithmeticObfuscator _arithmeticObfuscator; 
        private readonly Base64Obfuscator _base64Obfuscator; 
        private readonly Rot47Obfuscator _rot47Obfuscator; 
        private readonly DeObfuscator _deObfuscator; 
        public FileProcessingService() 
        { 
            _arithmeticObfuscator = new ArithmeticObfuscator(); 
            _base64Obfuscator = new Base64Obfuscator(); 
            _rot47Obfuscator = new Rot47Obfuscator(); 
            _deObfuscator = new DeObfuscator(); 
        } 
        public async Task<string> ObfuscateAsync(string content, string obfuscationType) 
        { 
            try 
            { 
                return await Task.Run(() => 
                { 
                    return obfuscationType switch 
                    { 
                        "arithmetic" => _arithmeticObfuscator.Obfuscate(content), 
                        "base64" => _base64Obfuscator.Obfuscate(content), 
                        "rot47" => _rot47Obfuscator.Obfuscate(content), 
                        _ => throw new ArgumentException("Invalid obfuscation type") 
                    }; 
                }); 
            } 
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Error in FileProcessingService.ObfuscateAsync: {ex}"); 
                throw; 
            } 
        } 
        public async Task<string> DeObfuscateAsync(string content) 
        { 
            try 
            { 
                return await Task.Run(() => _deObfuscator.DeObfuscate(content)); 
            } 
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Error in FileProcessingService.DeObfuscateAsync: {ex}"); 
                throw; 
            } 
        } 
    } 
}