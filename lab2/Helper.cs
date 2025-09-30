using System;

namespace lab1
{
    public class Helper
    {
        public void Run()
        {
            Polynomial p1 = new Polynomial(3);
            p1[0] = 1;
            p1[1] = 2;
            p1[2] = 3;

            Polynomial p2 = new Polynomial(2);
            p2[0] = 4;
            p2[1] = 5;

            Polynomial sum = p1 + p2;

            Console.WriteLine("Polynomial 1: " + p1);
            Console.WriteLine("Polynomial 2: " + p2);
            Console.WriteLine("Sum: " + sum);
        }
    }
}
