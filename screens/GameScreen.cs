using SDL2;

public class GameScreen : BaseScreen
{
  private Background background;
  private Paddle paddle;
  private List<Ball> balls = new List<Ball>();
  private List<Block> blocks = new List<Block>();
  private List<string[]> levels;
  private int currentLevelIndex = 0;
  private PerkManager perkManager;

  private LifeManager lifeManager;

  public GameScreen(IntPtr renderer) : base(renderer)
  {
    background = new Background(renderer);
    lifeManager = new LifeManager();
    paddle = new Paddle(renderer, (ScreenSize.Width / 2) - 50, ScreenSize.Height - 30);
    var initialBall = new Ball(renderer, paddle.Rect.x + (paddle.Rect.w / 2) - 5, paddle.Rect.y - 10, isOriginal: true, lifeManager);
    balls.Add(initialBall);

    perkManager = new PerkManager(paddle, renderer, balls, lifeManager);

    levels = LevelLoader.LoadLevels("./assets/levels/levels.json");

    if (levels.Count > 0)
    {
      blocks = BlockTemplateLoader.LoadBlocksFromTemplate(levels[currentLevelIndex], ScreenSize.Width, ScreenSize.Height);
    }
    else
    {
      Console.WriteLine("No levels found.");
    }
  }

  // Rendering of the game screen
  public override void Render()
  {
    background.Render();
    paddle.Render();
    lifeManager.Render(renderer);

    foreach (var ball in balls)
    {
      ball.Render();
    }

    foreach (var block in blocks)
    {
      block.Render(renderer);
    }
  }

  // Control of the paddle
  public override void HandleInput(object e)
  {
    var sdlEvent = (SDL.SDL_Event)e;
    if (sdlEvent.type == SDL.SDL_EventType.SDL_KEYDOWN)
    {
      if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_SPACE)
      {
        // Buscar la bola roja en el arreglo
        var redBall = balls.Find(ball => ball.IsOriginal);
        if (redBall != null && !redBall.InPlay)
        {
          redBall.Launch(paddle);
        }
      }
    }
  }

  // Update of the game state
  public override void Update()
  {
    paddle.Update();

    if (lifeManager.IsGameOver())
    {
      NextState = GameState.GameOver;
      return;
    }

    for (int i = balls.Count - 1; i >= 0; i--)
    {
      var ball = balls[i];
      ball.Update(paddle);

      // Si la bola ya no está en juego, la eliminamos del arreglo
      if (!ball.InPlay)
      {
        if (ball.IsOriginal)
        {
          // La vida ya se descontó en Ball.Update(), solo creamos una nueva bola roja
          var newBall = new Ball(renderer, paddle.Rect.x + (paddle.Rect.w / 2) - 5, paddle.Rect.y - 10, isOriginal: true, lifeManager);
          balls.Add(newBall);
          perkManager.ResetConsecutiveBlocks();
        }
        // Removemos la bola del arreglo
        balls.RemoveAt(i);
        continue; // Saltamos al siguiente ciclo ya que esta bola ya no existe
      }

      // Colisión con la pala
      if (ball.HandleCollisionWithPaddle(paddle))
      {
        perkManager.ResetConsecutiveBlocks();
      }

      // Colisiones con bloques
      for (int j = blocks.Count - 1; j >= 0; j--)
      {
        var block = blocks[j];
        if (block.IsVisible &&
            ball.Rect.x + ball.Rect.w > block.Rect.x && ball.Rect.x < block.Rect.x + block.Rect.w &&
            ball.Rect.y + ball.Rect.h > block.Rect.y && ball.Rect.y < block.Rect.y + block.Rect.h)
        {
          block.IsVisible = false;
          blocks.RemoveAt(j);
          ball.ReverseVerticalDirection();

          perkManager.BlockDestroyed(ball.IsOriginal);
          break;
        }
      }
    }

    if (blocks.Count == 0)
    {
      AdvanceToNextLevel();
    }
  }

  // Advance to the next level
  private void AdvanceToNextLevel()
  {
    currentLevelIndex++;
    if (currentLevelIndex < levels.Count)
    {
      blocks = BlockTemplateLoader.LoadBlocksFromTemplate(levels[currentLevelIndex], ScreenSize.Width, ScreenSize.Height);
      balls.Clear();
      var newBall = new Ball(renderer, paddle.Rect.x + (paddle.Rect.w / 2) - 5, paddle.Rect.y - 10, isOriginal: true, lifeManager);
      balls.Add(newBall);
    }
    else
    {
      Console.WriteLine("You completed all levels!");
    }
  }
}