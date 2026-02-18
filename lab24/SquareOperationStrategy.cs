namespace Lab24
{
    public class SquareOperationStrategy : INumericOperationStrategy
    {
        public string OperationName => "Квадрат";

        public double Execute(double value)
        {
            return value * value;
        }
    }
}
