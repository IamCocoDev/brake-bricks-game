using System.Text.Json;

public class LevelLoader
{
  public static List<string[]> LoadLevels(string filePath)
  {
    string json = File.ReadAllText(filePath);
    var levels = JsonSerializer.Deserialize<List<string[]>>(json);
    if (levels != null)
    {
      return levels;
    }
    else
    {
      return new List<string[]>();
    }
  }
}