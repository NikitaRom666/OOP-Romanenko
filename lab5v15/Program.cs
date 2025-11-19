// Program.cs
using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var office = new PostOffice("Центральне Поштове Відділення #15");
        
        //  1. Створення даних
        Console.WriteLine($"\n Завантаження даних для: {office.Name}...");
        try
        {
            // Успішні відправлення
            office.AddShipment(new Shipment("Львів", new DateTime(2025, 11, 1), new DateTime(2025, 11, 4))); 
            office.AddShipment(new Shipment("Київ", new DateTime(2025, 11, 10), new DateTime(2025, 11, 12))); 
            office.AddShipment(new Shipment("Львів", new DateTime(2025, 11, 15), new DateTime(2025, 11, 22))); 
            office.AddShipment(new Shipment("Одеса", new DateTime(2025, 10, 25), new DateTime(2025, 11, 1))); 
            
            // Проблемні (для розрахунку відсотків)
            var damaged = new Shipment("Київ", new DateTime(2025, 11, 1), new DateTime(2025, 11, 5)) { IsDamaged = true };
            office.AddShipment(damaged); 
            var lost = new Shipment("Харків", new DateTime(2025, 10, 1), null) { IsLost = true };
            office.AddShipment(lost);
            
            // Додатково для TopN
            office.AddShipment(new Shipment("Львів", new DateTime(2025, 11, 1), new DateTime(2025, 11, 3))); 
            
            Console.WriteLine($" Додано {office.GetAllShipments().Count()} записів.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Неочікувана помилка: {ex.Message}");
        }
        
        // 2. Обробка винятків (try-catch)
        Console.WriteLine("\n Демонстрація контролю вхідних даних:");
        try
        {
            // Дата доставки (2025-11-01) < Дата відправлення (2025-11-10) -> InvalidShipmentDatesException
            var invalidShipment = new Shipment(
                "Помилкове місто", 
                new DateTime(2025, 11, 10), new DateTime(2025, 11, 1)); 
        }
        catch (InvalidShipmentDatesException ex)
        {
            Console.WriteLine($" Власний виняток спіймано: {ex.Message}");
        }
        
        // 3. Обчислення з колекціями
        Console.WriteLine("\n Результати аналітики:");
        
        // Середній термін доставки
        TimeSpan avgTime = office.CalculateAverageDeliveryTerm();
        Console.WriteLine($"   Середній термін доставки: {avgTime.TotalDays:F2} днів");

        // Частка втрачених/пошкоджених
        var (lostP, damagedP) = office.GetIssueShares();
        Console.WriteLine($"   Частка втрачених: {lostP:F2}%");
        Console.WriteLine($"   Частка пошкоджених: {damagedP:F2}%");

        // Топ-2 напрямки (Generics TopN<T>)
        var topDestinations = office.FindTopDestinations(2);
        Console.WriteLine($"   Топ-2 напрямки за обсягом: {string.Join(", ", topDestinations)}");
    }
}