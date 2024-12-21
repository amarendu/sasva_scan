using System; 
public static class ArithmeticObfuscator 
{ 
    public static string Obfuscate(string input) 
    { 
        Random rand = new Random(); 
        string result = string.Empty; 
        foreach (char c in input) 
        { 
            int n = c; 
            int r = rand.Next(1, 10000); 
            int k = rand.Next(0, 3); 
            switch (k) 
            { 
                case 0: 
                    result += $"CLng(&H{(r + n):X})-{r}+"; 
                    break; 
                case 1: 
                    result += $"{n - r}+CLng(&H{r:X})+"; 
                    break; 
                default: 
                    result += $"{n * r}/CLng(&H{r:X})+"; 
                    break; 
            } 
        } 
        return $"Execute {result.TrimEnd('+')}"; 
    } 
} 