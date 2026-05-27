using System;
using System.Linq;

public class Polynomial
{
    private double[] coefficients;

    public Polynomial(int size)
    {
        coefficients = new double[size];
    }

    public double[] Coefficients
    {
        get => coefficients;
        set => coefficients = value;
    }

    public double this[int index]
    {
        get => coefficients[index];
        set => coefficients[index] = value;
    }

    public static Polynomial operator +(Polynomial a, Polynomial b)
    {
        int size = Math.Max(a.coefficients.Length, b.coefficients.Length);
        Polynomial result = new Polynomial(size);

        for (int i = 0; i < size; i++)
        {
            double valA = i < a.coefficients.Length ? a.coefficients[i] : 0;
            double valB = i < b.coefficients.Length ? b.coefficients[i] : 0;
            result[i] = valA + valB;
        }

        return result;
    }

    public override string ToString()
    {
        return string.Join(" + ", coefficients.Select((c, i) => $"{c}x^{i}"));
    }
}
