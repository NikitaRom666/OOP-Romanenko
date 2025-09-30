using System;

public class Figure
{
    public double Area { get; set; }

    public Figure(double area)
    {
        Area = area;
    }

    public string GetFigure()
    {
        return $"Figure (Area = {Area})";
    }
}
