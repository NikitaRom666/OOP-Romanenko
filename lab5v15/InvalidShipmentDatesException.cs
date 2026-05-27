// InvalidShipmentDatesException.cs
using System;

public class InvalidShipmentDatesException : Exception
{
    public DateTime DepartureDate { get; }
    public DateTime DeliveryDate { get; }

    public InvalidShipmentDatesException(DateTime departure, DateTime delivery)
        : base($"Помилка: Дата доставки ({delivery:yyyy-MM-dd}) не може бути раніше дати відправлення ({departure:yyyy-MM-dd}).")
    {
        DepartureDate = departure;
        DeliveryDate = delivery;
    }
}