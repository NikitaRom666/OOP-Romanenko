// PostOffice.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class PostOffice
{
    public string Name { get; }
    // Композиція: PostOffice містить List<Shipment>
    private readonly List<Shipment> _shipments = new List<Shipment>();

    public PostOffice(string name) => Name = name;

    public void AddShipment(Shipment shipment)
    {
        if (shipment == null) throw new ArgumentNullException(nameof(shipment));
        _shipments.Add(shipment);
    }

    public IEnumerable<Shipment> GetAllShipments() => _shipments;
    
    // Обчислення 1: Середній термін доставки
    public TimeSpan CalculateAverageDeliveryTerm()
    {
        // Фільтруємо лише доставлені
        var delivered = _shipments
            .Where(s => s.DeliveryDate.HasValue)
            .ToList();

        if (!delivered.Any())
        {
            return TimeSpan.Zero;
        }

        // Сума/Середнє (TotalDays)
        double totalDays = delivered
            .Select(s => s.GetDeliveryTime()?.TotalDays ?? 0)
            .Sum();
        
        return TimeSpan.FromDays(totalDays / delivered.Count);
    }

    // Обчислення 2: Частка втрачених/пошкоджених
    public (double LostPercentage, double DamagedPercentage) GetIssueShares()
    {
        if (!_shipments.Any())
        {
            return (0.0, 0.0);
        }

        int total = _shipments.Count;
        double lostPercent = (double)_shipments.Count(s => s.IsLost) / total * 100.0;
        double damagedPercent = (double)_shipments.Count(s => s.IsDamaged) / total * 100.0;
        
        return (lostPercent, damagedPercent);
    }
    
    // Обчислення 3: Топ-N напрямків (з використанням Generics)
    public IEnumerable<string> FindTopDestinations(int n)
    {
        if (!_shipments.Any())
        {
            return Enumerable.Empty<string>();
        }
        
        // Групування по напрямку
        var destinationVolumes = _shipments
            .GroupBy(s => s.Destination)
            .Select(g => new { Destination = g.Key, Volume = g.Count() });

        // Використовуємо узагальнений метод TopNByValue<T>
        var topDestinations = destinationVolumes
            .TopNByValue(n, item => item.Volume) // Використовуємо Volume як критерій
            .Select(item => item.Destination);

        return topDestinations;
    }
}