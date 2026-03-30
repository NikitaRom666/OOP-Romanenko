namespace Lab3v13;

/// <summary>
/// Клас для комедій, що успадковує клас Movie.
/// </summary>
public class ComedyMovie : Movie
{
    public string MainComedian { get; private set; }

    public ComedyMovie(string title, double rating, string mainComedian)
        // Виклик конструктора базового класу
        : base(title, rating)
    {
        MainComedian = mainComedian;
    }

    // Перевизначення абстрактного методу
    public override string GetGenre()
    {
        return "Комедія";
    }

    // Перевизначення віртуального методу
    public override string GetInfo()
    {
        return $"{base.GetInfo()}, Жанр: {GetGenre()}, Головний комік: {MainComedian}";
    }
}