// Shipment.cs
using System;

public class Shipment
{
    public Guid TrackingId { get; } = Guid.NewGuid();
    public string Destination { get; set; }
    public DateTime DepartureDate { get; private set; }
    public DateTime? DeliveryDate { get; private set; } 
    public bool IsLost { get; set; } // Для обчислення частки втрачених
    public bool IsDamaged { get; set; } // Для обчислення частки пошкоджених

    public Shipment(string destination, DateTime departure, DateTime? delivery = null)
    {
        // Контроль вхідних даних: кидаємо власний виняток
        if (delivery.HasValue && delivery.Value < departure)
        {
            throw new InvalidShipmentDatesException(departure, delivery.Value);
        }

        Destination = destination;
        DepartureDate = departure;
        DeliveryDate = delivery;
    }

    public TimeSpan? GetDeliveryTime() => DeliveryDate - DepartureDate;
}