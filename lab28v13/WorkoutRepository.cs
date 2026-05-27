using System.Text.Json;
using System.Text.Json.Serialization;

namespace lab28v13;

public class WorkoutRepository
{
    private List<Workout> _workouts = [];

    public void Add(Workout workout)
    {
        _workouts.Add(workout);
    }

    public List<Workout> GetAll()
    {
        return _workouts;
    }

    public Workout? GetById(int id)
    {
        return _workouts.FirstOrDefault(w => w.Id == id);
    }

    public async Task SaveToFileAsync(string filename)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var json = JsonSerializer.Serialize(_workouts, options);
        await File.WriteAllTextAsync(filename, json);
    }

    public async Task LoadFromFileAsync(string filename)
    {
        if (!File.Exists(filename))
            return;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var json = await File.ReadAllTextAsync(filename);
        var loaded = JsonSerializer.Deserialize<List<Workout>>(json, options);

        if (loaded != null)
        {
            _workouts = loaded;
        }
    }
}
