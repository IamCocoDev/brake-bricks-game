using System.Text.Json;

public class LevelLoader
{
  public static List<string[]> LoadLevels(string filePath)
  {
    string json = File.ReadAllText(filePath);
    var levels = JsonSerializer.Deserialize<List<string[]>>(json);
    if (levels != null)
    {
      Console.WriteLine($"Loaded {levels.Count} levels.");
      return levels;
    }
    else
    {
      Console.WriteLine("Failed to load levels or levels data is empty.");
      return new List<string[]>(); // Retorna una lista vacía si no se carga correctamente
    }
  }
}