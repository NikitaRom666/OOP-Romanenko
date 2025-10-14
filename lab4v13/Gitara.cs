// Файл: Gitara.cs
namespace Lab4v13
{
    // Клас, що описує гітару.
    public class Gitara : Interface
    {
        // Кількість струн.
        private readonly int _stringCount;

        public Gitara(int stringCount = 6)
        {
            _stringCount = stringCount;
        }

        // Реалізація гри на гітарі.
        public string Play()
        {
            return $"Звучать акорди {_stringCount}-струнної гітари.";
        }
    }
}