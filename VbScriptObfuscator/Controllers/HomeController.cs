using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Http; 
using System; 
using System.IO; 
using System.Threading.Tasks; 
using Microsoft.Extensions.Logging; 
using VbScriptObfuscator.Services; 
namespace VbScriptObfuscator.Controllers 
{ 
    public class HomeController : Controller 
    { 
        private readonly ILogger<HomeController> _logger; 
        private readonly IFileProcessingService _fileProcessingService; 
        public HomeController(ILogger<HomeController> logger, IFileProcessingService fileProcessingService) 
        { 
            _logger = logger; 
            _fileProcessingService = fileProcessingService; 
        } 
        public IActionResult Index() 
        { 
            return View(); 
        } 
        [HttpPost] 
        public async Task<IActionResult> UploadFiles(IFormFileCollection files, string obfuscationType) 
        { 
            if (files == null || files.Count == 0) 
            { 
                _logger.LogWarning("No files were uploaded."); 
                ViewData["Message"] = "Please upload at least one file."; 
                ViewData["MessageType"] = "warning"; 
                return View("Index"); 
            } 
            foreach (var file in files) 
            { 
                if (file.Length > 0) 
                { 
                    var filePath = Path.Combine(Path.GetTempPath(), file.FileName); 
                    try 
                    { 
                        using (var stream = new FileStream(filePath, FileMode.Create)) 
                        { 
                            await file.CopyToAsync(stream); 
                        } 
                        string fileContent; 
                        using (var reader = new StreamReader(filePath)) 
                        { 
                            fileContent = await reader.ReadToEndAsync(); 
                        } 
                        string obfuscatedContent = await _fileProcessingService.ObfuscateAsync(fileContent, obfuscationType); 
                        _logger.LogInformation("File {FileName} obfuscated successfully using {ObfuscationType}.", file.FileName, obfuscationType); 
                        ViewData["Message"] = $"File {file.FileName} obfuscated successfully."; 
                        ViewData["MessageType"] = "success"; 
                    } 
                    catch (Exception ex) 
                    { 
                        _logger.LogError(ex, "Error processing file {FileName}.", file.FileName); 
                        ViewData["Message"] = $"Error processing file {file.FileName}: {ex.Message}"; 
                        ViewData["MessageType"] = "error"; 
                        return View("Index"); 
                    } 
                } 
                else 
                { 
                    _logger.LogWarning("File {FileName} is empty.", file.FileName); 
                    ViewData["Message"] = $"File {file.FileName} is empty."; 
                    ViewData["MessageType"] = "warning"; 
                } 
            } 
            return View("Index"); 
        } 
        [HttpPost] 
        public async Task<IActionResult> DeObfuscateFiles(IFormFileCollection files) 
        { 
            if (files == null || files.Count == 0) 
            { 
                _logger.LogWarning("No files were uploaded."); 
                ViewData["Message"] = "Please upload at least one file."; 
                ViewData["MessageType"] = "warning"; 
                return View("Index"); 
            } 
            foreach (var file in files) 
            { 
                if (file.Length > 0) 
                { 
                    var filePath = Path.Combine(Path.GetTempPath(), file.FileName); 
                    try 
                    { 
                        using (var stream = new FileStream(filePath, FileMode.Create)) 
                        { 
                            await file.CopyToAsync(stream); 
                        } 
                        string fileContent; 
                        using (var reader = new StreamReader(filePath)) 
                        { 
                            fileContent = await reader.ReadToEndAsync(); 
                        } 
                        string deObfuscatedContent = await _fileProcessingService.DeObfuscateAsync(fileContent); 
                        _logger.LogInformation("File {FileName} de-obfuscated successfully.", file.FileName); 
                        ViewData["Message"] = $"File {file.FileName} de-obfuscated successfully."; 
                        ViewData["MessageType"] = "success"; 
                    } 
                    catch (Exception ex) 
                    { 
                        _logger.LogError(ex, "Error processing file {FileName}.", file.FileName); 
                        ViewData["Message"] = $"Error processing file {file.FileName}: {ex.Message}"; 
                        ViewData["MessageType"] = "error"; 
                        return View("Index"); 
                    } 
                } 
                else 
                { 
                    _logger.LogWarning("File {FileName} is empty.", file.FileName); 
                    ViewData["Message"] = $"File {file.FileName} is empty."; 
                    ViewData["MessageType"] = "warning"; 
                } 
            } 
            return View("Index"); 
        } 
    } 
}