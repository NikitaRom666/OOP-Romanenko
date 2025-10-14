// Файл: Gyrt.cs
using System.Collections.Generic;
using System;

namespace Lab4v13
{
    // Клас, що описує музичний гурт.
    public class Gyrt
    {
        public string Name { get; }
        // Список інструментів у гурті.
        private readonly List<Interface> _instruments;

        public Gyrt(string name)
        {
            Name = name;
            _instruments = new List<Interface>();
        }

        // Додає інструмент до гурту.
        public void AddInstrument(Interface instrument)
        {
            _instruments.Add(instrument);
        }

        // Імітує виступ гурту, граючи на всіх інструментах.
        public void Perform()
        {
            Console.WriteLine($"\nГурт '{Name}' починає виступ:");
            foreach (var instrument in _instruments)
            {
                Console.WriteLine(instrument.Play());
            }
        }
    }
}