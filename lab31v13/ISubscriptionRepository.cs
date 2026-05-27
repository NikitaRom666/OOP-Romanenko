namespace lab31v13;

public interface ISubscriptionRepository
{
    Subscription? GetById(int id);
    void Save(Subscription subscription);
    void Delete(int id);
    bool Exists(int id);
}