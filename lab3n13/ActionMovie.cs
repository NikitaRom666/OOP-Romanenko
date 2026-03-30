namespace Lab3v13;

/// <summary>
/// Клас для бойовиків, що успадковує клас Movie.
/// </summary>
public class ActionMovie : Movie
{
    public int ExplosionCount { get; private set; }

    public ActionMovie(string title, double rating, int explosionCount)
        // Виклик конструктора базового класу для ініціалізації спільних полів
        : base(title, rating)
    {
        ExplosionCount = explosionCount;
    }

    // Перевизначення (override) абстрактного методу базового класу
    public override string GetGenre()
    {
        return "Бойовик";
    }

    // Перевизначення (override) віртуального методу для додавання специфічної інформації
    public override string GetInfo()
    {
        // Виклик реалізації методу з базового класу та її доповнення
        return $"{base.GetInfo()}, Жанр: {GetGenre()}, Кількість вибухів: {ExplosionCount}";
    }
}