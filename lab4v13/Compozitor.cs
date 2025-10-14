// Файл: Compozitor.cs
namespace Lab4v13
{
    // Клас для зберігання даних про музичну композицію.
    public class Compozitor
    {
        public string Title { get; }
        public double DurationMinutes { get; }

        public Compozitor(string title, double durationMinutes)
        {
            Title = title;
            DurationMinutes = durationMinutes;
        }
    }
}