using SDL2;

public class Background
{
  private IntPtr renderer;
  private List<(int x, int y)> stars = new List<(int x, int y)>();

  public Background(IntPtr renderer, int numStars = 100)
  {
    this.renderer = renderer;
    InitializeStars(numStars);
  }

  private void InitializeStars(int numStars)
  {
    stars = new List<(int x, int y)>();
    Random random = new Random();
    for (int i = 0; i < numStars; i++)
    {
      int x = random.Next(0, 640);
      int y = random.Next(0, 900);
      stars.Add((x, y));
    }
  }

  public void Render()
  {
    SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
    SDL.SDL_RenderClear(renderer);
    SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
    foreach (var star in stars)
    {
      SDL.SDL_RenderDrawPoint(renderer, star.x, star.y);
    }
  }
}