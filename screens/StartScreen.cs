using SDL2;

public class StartScreen : BaseScreen
{
  private Background background;
  private SDL.SDL_Color currentColor;
  private int lastColorChangeInterval = -1;

  public StartScreen(IntPtr renderer) : base(renderer)
  {
    background = new Background(renderer);
  }

  public override void Render()
  {
    background.Render();

    Text.Render(renderer, IntPtr.Zero, "Press 'Space' to Start", "green", ScreenSize.Width / 2, 440);

    int brakeBricksX = ScreenSize.Width / 2 + (int)(50 * Math.Sin(SDL.SDL_GetTicks() / 200.0));

    int time = (int)SDL.SDL_GetTicks();
    int colorChangeInterval = 500;
    int currentInterval = time / colorChangeInterval;

    if (currentInterval != lastColorChangeInterval)
    {
      currentColor = Color.GetRandomColor(new string[] { "black", "white", "red", "green", "yellow" });
      lastColorChangeInterval = currentInterval;
    }

    Text.Render(renderer, IntPtr.Zero, "Brake Bricks", currentColor, brakeBricksX, 350, 40);
    Text.Render(renderer, IntPtr.Zero, "Press 'C' for Credits", "yellow", ScreenSize.Width / 2, 550);
    Text.Render(renderer, IntPtr.Zero, "Press 'ESC' to Exit", "red", ScreenSize.Width / 2, 600);
  }

  public override void HandleInput(object e)
  {
    var sdlEvent = (SDL.SDL_Event)e;
    if (sdlEvent.type == SDL.SDL_EventType.SDL_KEYDOWN)
    {
      if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_SPACE)
      {
        NextState = GameState.Playing;
      }
      else if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
      {
        NextState = GameState.Closing;
      }
      else if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_c)
      {
        NextState = GameState.Credits;
      }
    }
  }
}