using Lab3v13;

// Створюємо колекцію базового типу Movie для демонстрації поліморфізму
List<Movie> filmCollection = new List<Movie>
{
    new ActionMovie("Термінатор 2", 8.6, 30),
    new ComedyMovie("1+1", 8.5, "Омар Сі"),
    new ActionMovie("Міцний горішок", 8.2, 25),
    new ComedyMovie("Похмілля у Вегасі", 7.7, "Бредлі Купер")
};

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("--- Демонстрація поліморфізму ---\n");

// Для кожного об'єкта викликається його версія методу GetInfo()
foreach (var movie in filmCollection)
{
    Console.WriteLine(movie.GetInfo());
}

Console.WriteLine("\n--- Обчислення за варіантом ---\n");

// 1. Середній рейтинг за жанрами
var averageRatings = filmCollection
    .GroupBy(m => m.GetGenre())
    .Select(g => new { Genre = g.Key, AvgRating = g.Average(m => m.Rating) });

Console.WriteLine("Середній рейтинг за жанрами:");
foreach (var item in averageRatings)
{
    Console.WriteLine($"- {item.Genre}: {item.AvgRating:F2}");
}

// 2. Фільм з найвищим рейтингом
var topMovie = filmCollection.OrderByDescending(m => m.Rating).FirstOrDefault();

if (topMovie != null)
{
    Console.WriteLine("\nТоп-1 фільм за рейтингом:");
    Console.WriteLine($"- {topMovie.Title} ({topMovie.Rating:F1})");
}