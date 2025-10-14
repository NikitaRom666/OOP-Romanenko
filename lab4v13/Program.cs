// Файл: Program.cs
using System;
using Lab4v13;

class Program
{
    static void Main(string[] args)
    {
        // Встановив кодування для коректного відображення українських літер це я виконав для того щоб не було кирилиці :)
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створюємо об'єкти інструментів.
        Interface acousticGuitar = new Gitara(6);
        Interface grandPiano = new Pianino();

        // Створюємо гурт з новою назвою.
        var myBand = new Gyrt("NORD DIVISION");

        // Додаємо інструменти до гурту.
        myBand.AddInstrument(acousticGuitar);
        myBand.AddInstrument(grandPiano);

        // Створюємо концерт для цього гурту.
        var concert = new Concert(myBand);

        // Формуємо програму концерту з новими треками.
        concert.AddComposition(new Compozitor("Порция мести", 5.27)); // 5:16
        concert.AddComposition(new Compozitor("Прилет", 9.03));       // 9:02
        concert.AddComposition(new Compozitor("Контр наступ", 7.8));  // 7:48

        // Запускаємо концерт.
        concert.Start();
        // Вибрав рок музику яку слухають наші військові 
    }
}