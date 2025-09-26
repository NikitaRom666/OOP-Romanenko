using System;

namespace lab1
{
    public class Figure
    {
        private double area;

        public Figure(double area)
        {
            this.area = area >= 0 ? area : 0;
            Console.WriteLine($"[Figure] Конструктор: Area = {this.area}");
        }

        ~Figure()
        {
            // Destructor
        }

        public double Area
        {
            get { return area; }
            set { area = value >= 0 ? value : 0; }
        }

        public string GetFigure()
        {
            return $"Figure {{ Area = {area} }}";
        }
    }
}
