namespace Lab24
{
    public class CubeOperationStrategy : INumericOperationStrategy
    {
        public string OperationName => "Куб";

        public double Execute(double value)
        {
            return value * value * value;
        }
    }
}
