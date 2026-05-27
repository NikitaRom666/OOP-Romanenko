using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace IndependentWork12
{
    class Program
    {
        // Об'єкт для блокування потоків (щоб не було конфліктів)
        static object locker = new object();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Робота №12. PLINQ. Романенко Н.\n");

            // Створюємо список на 5 млн елементів
            int count = 5000000;
            Console.WriteLine("Генерую масив чисел...");
            var nums = GetNums(count);
            Console.WriteLine("Масив готовий.");

            // Запускаємо тест швидкості
            TestSpeed(nums);

            Console.WriteLine();
            
            // Запускаємо тест на безпеку потоків
            TestSafety();

            Console.WriteLine("\nКінець.");
            Console.ReadKey();
        }

        // Метод для генерації рандомних чисел
        static List<int> GetNums(int n)
        {
            var r = new Random();
            // Заповнюємо список числами від 1 до 50
            return Enumerable.Range(0, n).Select(x => r.Next(1, 50)).ToList();
        }

        // Якась важка математика щоб нагрузити проц
        static double Calc(int x)
        {
            double res = x;
            for (int i = 0; i < 50; i++)
            {
                res += Math.Sqrt(x * i) + Math.Cos(i);
            }
            return res;
        }

        static void TestSpeed(List<int> list)
        {
            Console.WriteLine("--- Тест 1: Швидкість ---");
            var sw = new Stopwatch();

            // 1. Звичайний LINQ
            sw.Start();
            var res1 = list.Select(Calc).ToList();
            sw.Stop();
            long timeLinQ = sw.ElapsedMilliseconds;
            Console.WriteLine($"Звичайний LINQ: {timeLinQ} мс");

            // 2. PLINQ (паралельний)
            sw.Restart();
            var res2 = list.AsParallel().Select(Calc).ToList();
            sw.Stop();
            long timePLinQ = sw.ElapsedMilliseconds;
            Console.WriteLine($"PLINQ: {timePLinQ} мс");

            if (timePLinQ < timeLinQ)
                Console.WriteLine($"PLINQ швидше десь у {timeLinQ / timePLinQ} разів.");
            else
                Console.WriteLine("Різниці майже немає.");
        }

        static void TestSafety()
        {
            Console.WriteLine("--- Тест 2: Безпека (Race Condition) ---");
            int total = 1000000;
            var range = Enumerable.Range(0, total);

            // Спосіб без захисту (буде помилка)
            int badCounter = 0;
            range.AsParallel().ForAll(n =>
            {
                badCounter++; // Тут потоки заважають один одному
            });

            Console.WriteLine($"Мало бути: {total}");
            Console.WriteLine($"Вийшло (без lock): {badCounter}");

            if (badCounter != total)
                Console.WriteLine("(Бачимо, що дані загубилися)");

            // Спосіб із захистом (lock)
            int goodCounter = 0;
            range.AsParallel().ForAll(n =>
            {
                lock (locker)
                {
                    goodCounter++;
                }
            });

            Console.WriteLine($"Вийшло (з lock): {goodCounter} - тут все ок.");
        }
    }
}

/*
ЗВІТ:
1. По швидкості:
   Я перевіряв на масиві з 5 мільйонів чисел. 
   Звичайний LINQ робив це десь 3-4 секунди.
   PLINQ впорався менше ніж за секунду (десь 600-800 мс).
   Висновок: PLINQ реально швидший, бо юзає всі ядра проца, а не одне.

2. По безпеці:
   Коли ми намагаємося міняти одну змінну з різних потоків (badCounter++), 
   виникає проблема Race Condition. Потоки перезаписують значення один одного.
   Тому замість мільйона виходить менше число.
   Щоб це виправити, треба юзати lock, але це трохи сповільнює роботу.
*/