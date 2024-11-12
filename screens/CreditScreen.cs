using SDL2;

public class CreditScreen : BaseScreen
{
  private Background background;
  public CreditScreen(IntPtr renderer) : base(renderer)
  {
    background = new Background(renderer);
  }

  public override void Initialize() { }

  public override void HandleInput(object e)
  {
    var sdlEvent = (SDL.SDL_Event)e;
    if (sdlEvent.type == SDL.SDL_EventType.SDL_KEYDOWN)
    {
      if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
      {
        NextState = GameState.StartScreen;
      }
    }
  }

  public override void Render()
  {
    background.Render();
    Text.Render(renderer, IntPtr.Zero, "Credits", "white", ScreenSize.Width / 2, ScreenSize.Height / 2 - 70, 50);
    Text.Render(renderer, IntPtr.Zero, "Developed by: Federico Giovenco", "yellow", ScreenSize.Width / 2, ScreenSize.Height / 2, 18);
    Text.Render(renderer, IntPtr.Zero, "Linkedin: /in/fgiovenco", "yellow", ScreenSize.Width / 2, ScreenSize.Height / 2 + 30, 18);
    Text.Render(renderer, IntPtr.Zero, "GitHub: iamcocodev", "yellow", ScreenSize.Width / 2, ScreenSize.Height / 2 + 60, 18);
    Text.Render(renderer, IntPtr.Zero, "Press ESC to go back", "green", ScreenSize.Width / 2, ScreenSize.Height / 2 + 200, 20);
  }
}
