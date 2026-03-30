// Файл: Concert.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4v13
{
    // Головний клас концерту.
    public class Concert
    {
        // Список пісень на вечір.
        private readonly List<Compozitor> _program;
        // Який гурт грає.
        private readonly Gyrt _band;

        public Concert(Gyrt band)
        {
            _band = band;
            _program = new List<Compozitor>();
        }

        // Додати пісню в програму.
        public void AddComposition(Compozitor composition)
        {
            _program.Add(composition);
        }

        // Рахуємо кількість пісень.
        public int GetNumberOfCompositions()
        {
            return _program.Count;
        }

        // Загальний час виступу.
        public double CalculateTotalDuration()
        {
            return _program.Sum(c => c.DurationMinutes);
        }

        // Початок виступу.
        public void Start()
        {
            _band.Perform();
            
            Console.WriteLine("\nПрограма виступу:");
            foreach (var composition in _program)
            {
                Console.WriteLine($"  -> {composition.Title} ({composition.DurationMinutes} хв.)");
            }

            Console.WriteLine("\nПідсумки концерту:");
            Console.WriteLine($"Кількість пісень: {GetNumberOfCompositions()}");
            // Ось тут я вствив змінну для файного виведення :)
            Console.WriteLine($"Загальна тривалість: {CalculateTotalDuration():F1} хв.");
        }
    }
}