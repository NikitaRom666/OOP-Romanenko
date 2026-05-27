namespace Lab24
{
    public interface INumericOperationStrategy
    {
        double Execute(double value);
        string OperationName { get; }
    }
}
