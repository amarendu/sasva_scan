using System; 
using System.Data; 
using System.Text.RegularExpressions; 
namespace VBSObfuscatorTool 
{ 
    public static class Deobfuscation 
    { 
        public static string Deobfuscate(string obfuscatedVbs) 
        { 
            try 
            { 
                Console.WriteLine("Starting deobfuscation process..."); 
                int executeIndex = obfuscatedVbs.IndexOf("Execute", StringComparison.OrdinalIgnoreCase); 
                if (executeIndex == -1) 
                { 
                    throw new InvalidOperationException("Invalid obfuscated script format."); 
                } 
                string scriptBody = obfuscatedVbs.Substring(executeIndex + "Execute".Length); 
                string deobfuscatedContent = EvaluateScript(scriptBody); 
                Console.WriteLine("Deobfuscation process completed successfully."); 
                return deobfuscatedContent; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error during deobfuscation: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
        private static string EvaluateScript(string scriptBody) 
        { 
            try 
            { 
                // Evaluate each chr expression and convert it back to the original character 
                string evaluatedContent = Regex.Replace(scriptBody, @"chr\((.*?)\)&", match => 
                { 
                    string expression = match.Groups[1].Value; 
                    int charCode = EvaluateExpression(expression); 
                    return ((char)charCode).ToString(); 
                }); 
                return evaluatedContent; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error evaluating script: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
        private static int EvaluateExpression(string expression) 
        { 
            try 
            { 
                // Use DataTable to compute the expression value 
                using (DataTable table = new DataTable()) 
                { 
                    object result = table.Compute(expression, string.Empty); 
                    return Convert.ToInt32(result); 
                } 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error evaluating expression: {ex.Message}"); 
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                throw; 
            } 
        } 
    } 
}