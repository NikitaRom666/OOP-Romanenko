using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Figure fig = new Figure(42.5);
            Console.WriteLine("Вивід: властивість Area та GetFigure():");
            Console.WriteLine($"Area property: {fig.Area}");
            Console.WriteLine(fig.GetFigure());
            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
