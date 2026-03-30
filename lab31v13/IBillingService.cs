namespace lab31v13;

public interface IBillingService
{
    bool Charge(int userId, decimal amount);
    void Refund(int userId, decimal amount);
    decimal GetBalance(int userId);
}