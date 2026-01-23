using System;

namespace lab21
{
    // 1. Інтерфейс стратегії
    public interface IShippingStrategy
    {
        decimal CalculateCost(decimal distance, decimal weight);
    }

    // 2. Реалізації різних алгоритмів (стратегій)
    public class StandardShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight) => 
            distance * 1.5m + weight * 0.5m;
    }

    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight) => 
            (distance * 2.5m + weight * 1.0m) + 50m;
    }

    public class InternationalShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight) => 
            (distance * 5.0m + weight * 2.0m) * 1.15m;
    }

    // Додаткова стратегія для демонстрації принципу відкритості/закритості (OCP)
    public class NightShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight) => 
            (distance * 1.5m + weight * 0.5m) + 100m;
    }

    // 3. Фабрика для створення об'єктів
    public static class ShippingStrategyFactory
    {
        public static IShippingStrategy CreateStrategy(string deliveryType)
        {
            return deliveryType.ToLower() switch
            {
                "standard" => new StandardShippingStrategy(),
                "express" => new ExpressShippingStrategy(),
                "international" => new InternationalShippingStrategy(),
                "night" => new NightShippingStrategy(),
                _ => throw new ArgumentException("Невідомий тип доставки!")
            };
        }
    }

    // 4. Клас-сервіс, який використовує стратегії
    public class DeliveryService
    {
        public decimal CalculateDeliveryCost(decimal distance, decimal weight, IShippingStrategy strategy)
        {
            return strategy.CalculateCost(distance, weight);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var service = new DeliveryService();

            try
            {
                Console.WriteLine("--- Система розрахунку доставки ---");
                Console.Write("Введіть тип (Standard, Express, International, Night): ");
                string type = Console.ReadLine();

                Console.Write("Введіть відстань (км): ");
                decimal dist = decimal.Parse(Console.ReadLine());

                Console.Write("Введіть вагу (кг): ");
                decimal weight = decimal.Parse(Console.ReadLine());

                // Отримуємо стратегію через фабрику
                IShippingStrategy strategy = ShippingStrategyFactory.CreateStrategy(type);
                
                // Розрахунок через сервіс
                decimal cost = service.CalculateDeliveryCost(dist, weight, strategy);

                Console.WriteLine($"\nРезультат: {cost:F2} грн.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
