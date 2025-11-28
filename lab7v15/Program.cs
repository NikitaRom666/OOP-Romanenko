using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace lab7v15
{
    // === 1. Клас RetryHelper ===
    public static class RetryHelper
    {
        public static T ExecuteWithRetry<T>(
            Func<T> operation, 
            int retryCount = 3, 
            TimeSpan initialDelay = default, 
            Func<Exception, bool> shouldRetry = null)
        {
            if (initialDelay == default) initialDelay = TimeSpan.FromSeconds(1);

            for (int attempt = 0; attempt <= retryCount; attempt++)
            {
                try
                {
                    return operation();
                }
                catch (Exception ex)
                {
                    if (attempt == retryCount || (shouldRetry != null && !shouldRetry(ex)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[Error] Остаточна помилка: {ex.Message}");
                        Console.ResetColor();
                        throw;
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[Log] Спроба {attempt + 1} невдала: {ex.Message}");
                    Console.ResetColor();

                    double multiplier = Math.Pow(2, attempt);
                    TimeSpan delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * multiplier);
                    Console.WriteLine($"[Wait] Чекаємо {delay.TotalSeconds} сек...");
                    Thread.Sleep(delay);
                }
            }
            throw new Exception("Невідома помилка");
        }
    }

    // === 2. Класи імітації (Варіант 15) ===
    public class FileProcessor
    {
        private int _count = 0;
        public string GetNotificationPayload(string path)
        {
            _count++;
            Console.WriteLine($"-> FileProcessor: Читання файлу (спроба {_count})...");
            if (_count <= 3) throw new IOException($"Файл '{path}' заблоковано.");
            return "{ \"msg\": \"Hello World\" }";
        }
    }

    public class NetworkClient
    {
        private int _count = 0;
        public void SendPushNotification(string id, string payload)
        {
            _count++;
            Console.WriteLine($"-> NetworkClient: Відправка (спроба {_count})...");
            if (_count <= 2) throw new HttpRequestException("Сервер 503.");
            Console.WriteLine("-> NetworkClient: Успішно!");
        }
    }

    // === 3. Main ===
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Лабораторна №7. Варіант 15. Романенко Нікіта\n");

            var file = new FileProcessor();
            var net = new NetworkClient();

            try
            {
                // КРОК 1
                Console.WriteLine("--- 1. Читаємо файл ---");
                string data = RetryHelper.ExecuteWithRetry(() => file.GetNotificationPayload("conf.json"), 
                    retryCount: 4, 
                    shouldRetry: ex => ex is IOException);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[OK] Дані: {data}\n");
                Console.ResetColor();

                // КРОК 2
                Console.WriteLine("--- 2. Відправляємо Push ---");
                RetryHelper.ExecuteWithRetry<bool>(() => 
                {
                    net.SendPushNotification("ID_123", data);
                    return true;
                }, retryCount: 3, shouldRetry: ex => ex is HttpRequestException);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[OK] Все зроблено!\n");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FATAL ERROR: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}