using System;
using System.Collections.Generic;

namespace lab20
{
    public enum OrderStatus { New, PendingValidation, Processed, Shipped, Delivered, Cancelled }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Order(int id, string customerName, decimal totalAmount)
        {
            Id = id;
            CustomerName = customerName;
            TotalAmount = totalAmount;
            Status = OrderStatus.New;
        }
    }

    public class OrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            if (order.TotalAmount <= 0)
            {
                Console.WriteLine("Помилка: невірна сума замовлення.");
                return;
            }
            Console.WriteLine($"Замовлення {order.Id} успішно збережено.");
            Console.WriteLine($"Email для {order.CustomerName} надіслано.");
            order.Status = OrderStatus.Processed;
        }
    }

    public interface IOrderValidator { bool IsValid(Order order); }
    public interface IOrderRepository { void Save(Order order); }
    public interface IEmailService { void SendOrderConfirmation(Order order); }

    public class OrderValidator : IOrderValidator
    {
        public bool IsValid(Order order) => order.TotalAmount > 0;
    }

    public class InMemoryOrderRepository : IOrderRepository
    {
        public void Save(Order order) => Console.WriteLine($"[Repo] Замовлення {order.Id} записано в базу.");
    }

    public class ConsoleEmailService : IEmailService
    {
        public void SendOrderConfirmation(Order order) => 
            Console.WriteLine($"[Email] Клієнт {order.CustomerName} отримав підтвердження.");
    }

    public class OrderService
    {
        private readonly IOrderValidator _validator;
        private readonly IOrderRepository _repo;
        private readonly IEmailService _email;

        public OrderService(IOrderValidator validator, IOrderRepository repo, IEmailService email)
        {
            _validator = validator;
            _repo = repo;
            _email = email;
        }

        public void ProcessOrder(Order order)
        {
            if (!_validator.IsValid(order))
            {
                Console.WriteLine($"[Service] Помилка: Замовлення {order.Id} не пройшло валідацію.");
                return;
            }

            _repo.Save(order);
            _email.SendOrderConfirmation(order);
            order.Status = OrderStatus.Processed;
            Console.WriteLine("[Service] Обробка замовлення завершена.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Перевірка роботи сервісу (SRP)");
            
            var service = new OrderService(
                new OrderValidator(), 
                new InMemoryOrderRepository(), 
                new ConsoleEmailService()
            );

            // Спроба обробити правильне замовлення
            var validOrder = new Order(101, "Нікіта Романенко", 1250.50m);
            service.ProcessOrder(validOrder);

            Console.WriteLine();

        
            var invalidOrder = new Order(102, "Тестовий Клієнт", -50);
            service.ProcessOrder(invalidOrder);

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}