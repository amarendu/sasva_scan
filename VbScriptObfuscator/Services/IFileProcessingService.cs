namespace VbScriptObfuscator.Services 
{ 
    public interface IFileProcessingService 
    { 
        Task<string> ObfuscateAsync(string content, string obfuscationType); 
        Task<string> DeObfuscateAsync(string content); 
    } 
}