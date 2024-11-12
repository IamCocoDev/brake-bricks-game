using SDL2;
public static class Color
{
  private static readonly Dictionary<string, SDL.SDL_Color> Colors = new Dictionary<string, SDL.SDL_Color>
    {
        { "white", new SDL.SDL_Color { r = 255, g = 255, b = 255, a = 255 } },
        { "black", new SDL.SDL_Color { r = 0, g = 0, b = 0, a = 255 } },
        { "red", new SDL.SDL_Color { r = 255, g = 0, b = 0, a = 255 } },
        { "green", new SDL.SDL_Color { r = 0, g = 255, b = 0, a = 255 } },
        { "blue", new SDL.SDL_Color { r = 0, g = 0, b = 255, a = 255 } },
        { "yellow", new SDL.SDL_Color { r = 255, g = 255, b = 0, a = 255 } },
        { "cyan", new SDL.SDL_Color { r = 0, g = 255, b = 255, a = 255 } },
        { "magenta", new SDL.SDL_Color { r = 255, g = 0, b = 255, a = 255 } },
        { "gray", new SDL.SDL_Color { r = 128, g = 128, b = 128, a = 255 } },
        { "lightgray", new SDL.SDL_Color { r = 211, g = 211, b = 211, a = 255 } },
        { "darkgray", new SDL.SDL_Color { r = 169, g = 169, b = 169, a = 255 } },
        { "orange", new SDL.SDL_Color { r = 255, g = 165, b = 0, a = 255 } },
        { "purple", new SDL.SDL_Color { r = 128, g = 0, b = 128, a = 255 } },
        { "brown", new SDL.SDL_Color { r = 165, g = 42, b = 42, a = 255 } },
        { "pink", new SDL.SDL_Color { r = 255, g = 192, b = 203, a = 255 } },
        { "lime", new SDL.SDL_Color { r = 0, g = 255, b = 0, a = 255 } },
        { "olive", new SDL.SDL_Color { r = 128, g = 128, b = 0, a = 255 } }
    };

  private static readonly Random Random = new Random();

  public static SDL.SDL_Color GetColor(string colorName)
  {
    if (Colors.TryGetValue(colorName.ToLower(), out SDL.SDL_Color color))
    {
      return color;
    }
    return Colors["black"];
  }

  public static SDL.SDL_Color GetRandomColor(string[] excludedColors)
  {
    List<SDL.SDL_Color> colorValues = new List<SDL.SDL_Color>(Colors.Values);
    foreach (var excludedColor in excludedColors)
    {
      colorValues.Remove(Colors[excludedColor]);
    }
    int randomIndex = Random.Next(colorValues.Count);
    return colorValues[randomIndex];
  }

}