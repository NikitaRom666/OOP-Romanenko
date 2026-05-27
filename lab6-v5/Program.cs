using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6
{
    // Оголошуємо свій делегат (як в умові)
    public delegate decimal CountBonus(decimal zp);

    // Клас для співробітників
    class Employee
    {
        public string Name { get; set; }
        public string Job { get; set; } // скоротив назву
        public decimal Salary { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створюємо список, заповнюємо даними
            List<Employee> emps = new List<Employee>
            {
                new Employee { Name = "Олег", Job = "Junior", Salary = 8000 },
                new Employee { Name = "Марія", Job = "Senior", Salary = 15000 },
                new Employee { Name = "Іван", Job = "Manager", Salary = 12000 },
                new Employee { Name = "Катя", Job = "HR", Salary = 9500 },
                new Employee { Name = "Діма", Job = "Team Lead", Salary = 18000 }
            };

            Console.WriteLine("Всі працівники створені.\n");

            // 1. Використання Func та Action (Варіант 5) 
            
            // Func: перевіряємо, чи зп більше 10к
            Func<Employee, bool> checkSalary = x => x.Salary > 10000;

            // Action: просто виводимо в консоль
            Action<Employee> showInfo = x => Console.WriteLine($"Багатий: {x.Name} ({x.Salary})");

            Console.WriteLine("--- Зарплата > 10000 ---");
            
            // Фільтруємо через LINQ і Where
            var richList = emps.Where(checkSalary).ToList();

            // Виводимо через Action
            foreach (var item in richList)
            {
                showInfo(item);
            }

            // 2. Predicate та анонімний метод 
            Console.WriteLine("\n--- Пошук Manager (Predicate) ---");

            // Тут використав анонімний метод замість лямбди для різноманітності
            Predicate<Employee> isManager = delegate (Employee e)
            {
                return e.Job == "Manager";
            };

            var managers = emps.FindAll(isManager);
            foreach (var m in managers)
            {
                Console.WriteLine($"Знайшли менеджера: {m.Name}");
            }

            // 3. Власний делегат і бонус 
            Console.WriteLine("\n--- Рахуємо бонуси ---");

            // Лямбда для розрахунку (10 відсотків)
            CountBonus calc = s => s * 0.1m;

            foreach (var w in emps)
            {
                decimal bonus = calc(w.Salary);
                // Console.WriteLine(bonus); // перевірка
            }
            Console.WriteLine("Бонуси пораховані у фоні.");

            // 4. LINQ (сортування і агрегація) 
            Console.WriteLine("\n--- Статистика ---");

            // Сортуємо по імені
            var sorted = emps.OrderBy(x => x.Name).Select(x => x.Name);
            Console.WriteLine("По алфавіту: " + string.Join(", ", sorted));

            // Рахуємо загальну суму зарплат через Aggregate
            decimal total = emps.Aggregate(0m, (sum, x) => sum + x.Salary);
            Console.WriteLine($"Разом треба платити: {total}");

            // Бонусний бал: комбінований делегат
            Console.WriteLine("\n--- Лог (Multicast Delegate) ---");
            Action<string> log = str => Console.Write("Log: " + str);
            log += str => Console.WriteLine(" [OK]"); // додаємо ще одну дію
            
            log("Кінець роботи");

            Console.ReadLine();
        }
    }
}