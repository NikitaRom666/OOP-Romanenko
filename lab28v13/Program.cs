using lab28v13;

var repo = new WorkoutRepository();

var pushUp = new Exercise
{
    Id = 1,
    Name = "Push-up",
    Description = "Standard push-up",
    TargetMuscle = MuscleGroup.Chest,
    DurationSeconds = 60,
    CaloriesBurned = 15
};

var pullUp = new Exercise
{
    Id = 2,
    Name = "Pull-up",
    Description = "Standard pull-up",
    TargetMuscle = MuscleGroup.Back,
    DurationSeconds = 60,
    CaloriesBurned = 12
};

var plank = new Exercise
{
    Id = 3,
    Name = "Plank",
    Description = "Core plank",
    TargetMuscle = MuscleGroup.Core,
    DurationSeconds = 90,
    CaloriesBurned = 8
};

var workout1 = new Workout
{
    Id = 1,
    Title = "Morning Strength",
    Date = DateTime.Today,
    TrainerName = "Alex",
    Exercises = [pushUp, pullUp, plank]
};

var running = new Exercise
{
    Id = 4,
    Name = "Running",
    Description = "30 min cardio run",
    TargetMuscle = MuscleGroup.Cardio,
    DurationSeconds = 1800,
    CaloriesBurned = 250
};

var jumpRope = new Exercise
{
    Id = 5,
    Name = "Jump rope",
    Description = "Jump rope cardio",
    TargetMuscle = MuscleGroup.Cardio,
    DurationSeconds = 600,
    CaloriesBurned = 80
};

var workout2 = new Workout
{
    Id = 2,
    Title = "Cardio Day",
    Date = DateTime.Today.AddDays(1),
    TrainerName = "Maria",
    Exercises = [running, jumpRope]
};

repo.Add(workout1);
repo.Add(workout2);

await repo.SaveToFileAsync("workouts.json");
Console.WriteLine("Saved to workouts.json\n");

var loadedRepo = new WorkoutRepository();
await loadedRepo.LoadFromFileAsync("workouts.json");

var allWorkouts = loadedRepo.GetAll();
foreach (var workout in allWorkouts)
{
    Console.WriteLine($"Title: {workout.Title}");
    Console.WriteLine($"Date: {workout.Date:dd.MM.yyyy}");
    Console.WriteLine($"Trainer: {workout.TrainerName}");
    Console.WriteLine($"Exercises: {workout.Exercises.Count}");
    Console.WriteLine($"Total duration: {workout.TotalDurationSeconds / 60.0:F1} minutes");
    Console.WriteLine($"Total calories: {workout.TotalCalories} kcal");

    foreach (var exercise in workout.Exercises)
    {
        Console.WriteLine($"  {exercise.Name} — {exercise.TargetMuscle} — {exercise.CaloriesBurned} kcal");
    }

    Console.WriteLine();
}

var foundWorkout = loadedRepo.GetById(1);
if (foundWorkout != null)
{
    Console.WriteLine($"GetById(1): {foundWorkout.Title}");
}

var notFound = loadedRepo.GetById(99);
if (notFound == null)
{
    Console.WriteLine("GetById(99): Not found");
}
