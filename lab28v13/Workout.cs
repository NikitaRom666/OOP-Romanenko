namespace lab28v13;

public class Workout
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string TrainerName { get; set; } = string.Empty;
    public List<Exercise> Exercises { get; set; } = [];

    public int TotalCalories => Exercises.Sum(e => e.CaloriesBurned);

    public int TotalDurationSeconds => Exercises.Sum(e => e.DurationSeconds);
}
