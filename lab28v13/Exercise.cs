namespace lab28v13;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public MuscleGroup TargetMuscle { get; set; }
    public int DurationSeconds { get; set; }
    public int CaloriesBurned { get; set; }
}
