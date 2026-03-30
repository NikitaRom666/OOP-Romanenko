namespace lab31v13;

public class SubscriptionService
{
    private readonly ISubscriptionRepository _repo;
    private readonly IBillingService _billing;

    public SubscriptionService(ISubscriptionRepository repo, IBillingService billing)
    {
        _repo = repo;
        _billing = billing;
    }

    public Subscription GetSubscription(int id)
    {
        var subscription = _repo.GetById(id);
        if (subscription is null)
        {
            throw new KeyNotFoundException($"Subscription with id {id} was not found.");
        }

        return subscription;
    }

    public bool Subscribe(int userId, string plan, decimal price)
    {
        var charged = _billing.Charge(userId, price);
        if (!charged)
        {
            return false;
        }

        var now = DateTime.UtcNow;
        var subscription = new Subscription
        {
            UserId = userId,
            Plan = plan,
            StartDate = now,
            EndDate = now.AddDays(30),
            IsActive = true
        };

        _repo.Save(subscription);
        return true;
    }

    public bool CancelSubscription(int id)
    {
        var subscription = GetSubscription(id);
        subscription.IsActive = false;
        _repo.Save(subscription);

        const decimal basePrice = 10m;
        var totalDays = Math.Max((decimal)(subscription.EndDate - subscription.StartDate).TotalDays, 0m);
        var remainingDays = Math.Max((decimal)(subscription.EndDate - DateTime.UtcNow).TotalDays, 0m);

        var refundAmount = 0m;
        if (totalDays > 0m && remainingDays > 0m)
        {
            refundAmount = (remainingDays / totalDays) * basePrice;
        }

        _billing.Refund(subscription.UserId, refundAmount);
        return true;
    }

    public bool RenewSubscription(int id, decimal price)
    {
        var subscription = GetSubscription(id);
        var charged = _billing.Charge(subscription.UserId, price);
        if (!charged)
        {
            return false;
        }

        subscription.EndDate = subscription.EndDate.AddDays(30);
        _repo.Save(subscription);
        return true;
    }

    public bool IsActive(int id)
    {
        try
        {
            return GetSubscription(id).IsActive;
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
    }
}