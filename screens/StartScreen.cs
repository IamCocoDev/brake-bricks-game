using SDL2;

public class StartScreen : BaseScreen
{
  private Background background;

  public StartScreen(IntPtr renderer) : base(renderer)
  {
    background = new Background(renderer);
  }

  public override void Render()
  {
    background.Render();

    Text.Render(renderer, IntPtr.Zero, "Welcome to Brake Bricks!", "white", ScreenSize.Width / 2, 400);
    Text.Render(renderer, IntPtr.Zero, "Press 'Space' to Start", "green", ScreenSize.Width / 2, 440);
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
    }
  }
}