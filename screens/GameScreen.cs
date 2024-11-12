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
  private ScoreManager scoreManager;

  public GameScreen(IntPtr renderer) : base(renderer)
  {
    background = new Background(renderer);
    lifeManager = new LifeManager();
    scoreManager = new ScoreManager();
    paddle = new Paddle(renderer, (ScreenSize.Width / 2) - 50, ScreenSize.Height - 30);
    var initialBall = new Ball(renderer, paddle.Rect.x + (paddle.Rect.w / 2) - 5, paddle.Rect.y - 10, isOriginal: true, lifeManager, scoreManager);
    balls.Add(initialBall);

    perkManager = new PerkManager(paddle, renderer, balls, lifeManager, scoreManager);

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

  public override void Render()
  {
    background.Render();
    paddle.Render();
    lifeManager.Render(renderer);
    scoreManager.Render(renderer);


    foreach (var ball in balls)
    {
      ball.Render();
    }

    foreach (var block in blocks)
    {
      block.Render(renderer);
    }
  }

  public override void HandleInput(object e)
  {
    var sdlEvent = (SDL.SDL_Event)e;
    if (sdlEvent.type == SDL.SDL_EventType.SDL_KEYDOWN)
    {
      if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_SPACE)
      {
        var redBall = balls.Find(ball => ball.IsOriginal);
        if (redBall != null && !redBall.InPlay)
        {
          redBall.Launch(paddle);
        }
      }
    }
  }

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
      ball.Update(paddle, blocks, perkManager, scoreManager, lifeManager);
      if (!ball.InPlay)
      {
        if (ball.IsOriginal)
        {
          var newBall = new Ball(renderer, paddle.Rect.x + (paddle.Rect.w / 2) - 5, paddle.Rect.y - 10, isOriginal: true, lifeManager, scoreManager);
          balls.Add(newBall);
          perkManager.ResetConsecutiveBlocks();
        }
        balls.RemoveAt(i);
        continue;
      }
    }

    if (blocks.Count == 0)
    {
      AdvanceToNextLevel();
    }
  }

  private void AdvanceToNextLevel()
  {
    currentLevelIndex++;
    if (currentLevelIndex < levels.Count)
    {
      blocks = BlockTemplateLoader.LoadBlocksFromTemplate(levels[currentLevelIndex], ScreenSize.Width, ScreenSize.Height);
      balls.Clear();
      var newBall = new Ball(renderer, paddle.Rect.x + (paddle.Rect.w / 2) - 5, paddle.Rect.y - 10, isOriginal: true, lifeManager, scoreManager);
      balls.Add(newBall);
    }
    else
    {
      NextState = GameState.Victory;
    }
  }

  public int GetCurrentScore()
  {
    return scoreManager.Score;
  }
}