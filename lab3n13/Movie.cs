namespace Lab3v13;

/// <summary>
/// Абстрактний базовий клас для представлення фільму.
/// </summary>
public abstract class Movie
{
    public string Title { get; protected set; }
    public double Rating { get; protected set; }

    // Конструктор базового класу
    public Movie(string title, double rating)
    {
        Title = title;
        Rating = rating;
    }

    /// <summary>
    /// Абстрактний метод, який ПОВИНЕН бути реалізований у похідних класах.
    /// </summary>
    public abstract string GetGenre();

    /// <summary>
    /// Віртуальний метод, який МОЖЕ бути перевизначений у похідних класах.
    /// </summary>
    public virtual string GetInfo()
    {
        return $"Назва: {Title}, Рейтинг: {Rating:F1}";
    }
}