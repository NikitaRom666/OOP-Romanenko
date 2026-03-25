namespace lab31v13;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Plan { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}