using SDL2;

public class GameOverScreen : BaseScreen
{
  private int score;
  private bool victory;

  public GameOverScreen(IntPtr renderer, int score, bool victory) : base(renderer)
  {
    this.score = score;
    this.victory = victory;
  }

  public override void Initialize() { }

  public override void HandleInput(object e)
  {
    var sdlEvent = (SDL.SDL_Event)e;
    if (sdlEvent.type == SDL.SDL_EventType.SDL_KEYDOWN)
    {
      if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_LSHIFT)
      {
        NextState = GameState.StartScreen;
      }
    }
  }

  public override void Render()
  {
    string message = victory ? "You Won!" : "Game Over";
    string messageColor = victory ? "green" : "red";
    Text.Render(renderer, IntPtr.Zero, message, messageColor, ScreenSize.Width / 2, ScreenSize.Height / 2 - 70, 50);
    Text.Render(renderer, IntPtr.Zero, $"Your Score: {score}", "yellow", ScreenSize.Width / 2, ScreenSize.Height / 2);
    Text.Render(renderer, IntPtr.Zero, "Press L-Shift to Save & Restart", "green", ScreenSize.Width / 2, ScreenSize.Height / 2 + 70, 20);
  }
}