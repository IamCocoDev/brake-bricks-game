using SDL2;
using System;

public static class Text
{
  private static string defaultFontPath = "Assets/Fonts/PressStart2P-Regular.ttf";
  private static IntPtr defaultFont;

  static Text()
  {
    defaultFont = SDL_ttf.TTF_OpenFont(defaultFontPath, 24);
    if (defaultFont == IntPtr.Zero)
    {
      Console.WriteLine($"Error loading default font: {SDL.SDL_GetError()}");
    }
  }

  public static void Render(IntPtr renderer, IntPtr font, string message, string colorName, int x, int y, int? fontSize = null)
  {
    if (font == IntPtr.Zero)
    {
      font = defaultFont;
    }

    if (fontSize.HasValue)
    {
      font = SDL_ttf.TTF_OpenFont(defaultFontPath, fontSize.Value);
      if (font == IntPtr.Zero)
      {
        Console.WriteLine($"Error loading font with size {fontSize.Value}: {SDL.SDL_GetError()}");
        return;
      }
    }

    SDL.SDL_Color color = Color.GetColor(colorName);

    IntPtr surface = SDL_ttf.TTF_RenderText_Solid(font, message, color);
    if (surface == IntPtr.Zero)
    {
      Console.WriteLine($"Error creating text surface: {SDL.SDL_GetError()}");
      return;
    }

    IntPtr texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
    if (texture == IntPtr.Zero)
    {
      Console.WriteLine($"Error creating texture: {SDL.SDL_GetError()}");
      SDL.SDL_FreeSurface(surface);
      return;
    }

    SDL.SDL_FreeSurface(surface);

    SDL.SDL_QueryTexture(texture, out _, out _, out int width, out int height);
    SDL.SDL_Rect dstRect = new SDL.SDL_Rect { x = x - width / 2, y = y - height / 2, w = width, h = height };

    SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref dstRect);

    SDL.SDL_DestroyTexture(texture);

    if (fontSize.HasValue)
    {
      SDL_ttf.TTF_CloseFont(font);
    }
  }

  // Destructor for cleanup
  public static void Cleanup()
  {
    if (defaultFont != IntPtr.Zero)
    {
      SDL_ttf.TTF_CloseFont(defaultFont);
    }
  }
}