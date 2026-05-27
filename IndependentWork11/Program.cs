using System;
using System.Net.Http;
using System.Threading;
using Polly; // Підключаємо Polly
using Polly.CircuitBreaker; // Для Circuit Breaker

namespace IndependentWork11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Самостійна робота №11. Використання Polly. \n");

            // --- ЗАПУСК СЦЕНАРІЮ 1 ---
            RunRetryScenario();

            Console.WriteLine(new string('-', 50));

            // --- ЗАПУСК СЦЕНАРІЮ 2 ---
            RunCircuitBreakerScenario();

            Console.WriteLine("\nРоботу завершено.");
        }

        // =========================================================
        // СЦЕНАРІЙ 1: RETRY (ПОВТОР)
        // =========================================================
        /* ЗВІТ ДО СЦЕНАРІЮ 1
         * 1. Проблема: Тимчасова втрата з'єднання з базою даних або API. 
         * Якщо викинути помилку одразу, користувач втратить дані.
         * 2. Політика: Retry (WaitAndRetry) з експоненційною затримкою.
         * Ми чекаємо довше з кожною спробою (1с -> 2с -> 4с), щоб дати серверу час відновитися.
         * 3. Очікувана поведінка: Програма зробить 3 спроби. Перші дві будуть невдалі, третя - успішна.
         */
        static void RunRetryScenario()
        {
            Console.WriteLine("\n>>> Сценарій 1: Підключення до БД (Retry) <<<");

            int attempts = 0;

            // Налаштування політики: Ловимо Exception, робимо 3 повтори з паузами
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryCount: 3, 
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // 2^1, 2^2...
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[LOG] Спроба #{retryCount} невдала. Чекаємо {timeSpan.TotalSeconds} сек... (Помилка: {exception.Message})");
                        Console.ResetColor();
                    });

            try
            {
                retryPolicy.Execute(() =>
                {
                    attempts++;
                    Console.WriteLine($"-> Спроба підключення №{attempts}...");

                    // Імітація: перші 2 рази - помилка
                    if (attempts <= 2) throw new Exception("Connection timeout");

                    Console.WriteLine("-> Успішне підключення!");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FATAL: {ex.Message}");
            }
        }

        // =========================================================
        // СЦЕНАРІЙ 2: CIRCUIT BREAKER (ЗАПОБІЖНИК)
        // =========================================================
        /* ЗВІТ ДО СЦЕНАРІЮ 2
         * 1. Проблема: Зовнішній сервіс "впав" повністю. Постійні спроби підключитися (Retry) 
         * тільки перевантажують нашу систему і "мертвий" сервіс.
         * 2. Політика: Circuit Breaker.
         * Якщо сталося 2 помилки поспіль, ми "вибиваємо пробки" на 5 секунд. 
         * У цей час всі запити відхиляються миттєво, не чекаючи відповіді сервера.
         * 3. Очікувана поведінка: Після 2 помилок система скаже "Circuit is Open" і не буде робити запити.
         */
        static void RunCircuitBreakerScenario()
        {
            Console.WriteLine("\n>>> Сценарій 2: API Запобіжник (Circuit Breaker) <<<");

            // Налаштування: Після 2 помилок блокуємо на 5 секунд
            var breakerPolicy = Policy
                .Handle<HttpRequestException>()
                .CircuitBreaker(
                    exceptionsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromSeconds(5),
                    onBreak: (ex, timespan) => 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[STOP] Запобіжник спрацював! Система заблокована на {timespan.TotalSeconds} сек.");
                        Console.ResetColor();
                    },
                    onReset: () => Console.WriteLine("[RESET] Система відновилася!"),
                    onHalfOpen: () => Console.WriteLine("[TEST] Тестовий запит (Half-Open)...")
                );

            // Робимо серію запитів у циклі
            for (int i = 0; i < 7; i++)
            {
                try
                {
                    Thread.Sleep(1000); // Пауза між запитами
                    Console.Write($"Запит #{i + 1}: ");

                    breakerPolicy.Execute(() =>
                    {
                        // Імітація: Сервіс лежить перші 5 секунд
                        if (i < 5) throw new HttpRequestException("500 Server Error");
                        Console.WriteLine("Успіх (200 OK)");
                    });
                }
                catch (BrokenCircuitException)
                {
                    Console.WriteLine("Блокування (Circuit Open). Запит скасовано.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }
    }
}