using System;

class Program
{
    static void Main(string[] args)
    {
        Figure myFigure = new Figure(50.5);
        string figureInfo = myFigure.GetFigure();
        Console.WriteLine(figureInfo);
    }
}
