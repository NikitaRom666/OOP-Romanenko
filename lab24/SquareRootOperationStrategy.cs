namespace Lab24
{
    public class SquareRootOperationStrategy : INumericOperationStrategy
    {
        public string OperationName => "Квадратний корінь";

        public double Execute(double value)
        {
            if (value < 0)
                throw new ArgumentException("Не можна обчислити квадратний корінь з від'ємного числа");
            return Math.Sqrt(value);
        }
    }
}
