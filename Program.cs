using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== MyDockerApp запущено ===");
        Console.WriteLine($"Середовище: {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development"}");

      
        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            Console.WriteLine("Застосунок коректно завершує роботу...");
        };

        Console.CancelKeyPress += (s, e) =>
        {
            e.Cancel = true;
            Console.WriteLine("Отримано Ctrl+C, завершення...");
        };

        
        string dataDir = Path.Combine(AppContext.BaseDirectory, "data");
        Directory.CreateDirectory(dataDir);

        string outputPath = Path.Combine(dataDir, "output.txt");
        string content = $"Запуск о {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        File.AppendAllText(outputPath, content + Environment.NewLine);

        Console.WriteLine($"Записано у файл: {outputPath}");
        Console.WriteLine("Зміст файлу:");
        Console.WriteLine(File.ReadAllText(outputPath));

        Console.WriteLine("Завершено");
    }
}