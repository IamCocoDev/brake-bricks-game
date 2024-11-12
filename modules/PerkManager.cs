public class PerkManager
{
  private Paddle paddle;
  private IntPtr renderer;
  private int consecutiveBlocksDestroyed;
  private int perkThreshold;
  private List<Ball> balls;
  private LifeManager lifeManager;
  private ScoreManager scoreManager;
  private bool perkActive;

  public PerkManager(Paddle paddle, IntPtr renderer, List<Ball> balls, LifeManager lifeManager, ScoreManager scoreManager, int perkThreshold = 3)
  {
    this.paddle = paddle;
    this.renderer = renderer;
    this.balls = balls;
    this.lifeManager = lifeManager;
    this.perkThreshold = perkThreshold;
    this.scoreManager = scoreManager;
    consecutiveBlocksDestroyed = 0;
    perkActive = false;
  }
  public void BlockDestroyed(bool isOriginalBall)
  {
    if (!isOriginalBall) return;

    consecutiveBlocksDestroyed++;

    if (consecutiveBlocksDestroyed >= perkThreshold && GetActiveGrayscaleBallCount() < 2)
    {
      GrantPerk();
      consecutiveBlocksDestroyed = 0;
    }
  }

  public void ResetConsecutiveBlocks()
  {
    consecutiveBlocksDestroyed = 0;
    perkActive = false;
  }

  private void GrantPerk()
  {
    Console.WriteLine("Perk unlocked: Additional Ball!");
    perkActive = true;

    int ballsToCreate = Math.Min(1, 2 - GetActiveGrayscaleBallCount());
    for (int i = 0; i < ballsToCreate; i++)
    {
      var newBall = new Ball(renderer, paddle.Rect.x + (paddle.Rect.w / 2) - 5, paddle.Rect.y - 10, isOriginal: false, lifeManager, scoreManager);
      newBall.Launch(paddle);
      balls.Add(newBall);
    }
  }

  private int GetActiveGrayscaleBallCount()
  {
    int count = 0;
    foreach (var ball in balls)
    {
      if (!ball.IsOriginal && ball.InPlay)
      {
        count++;
      }
    }
    return count;
  }

  public void Render(IntPtr renderer)
  {
    if (perkActive)
    {
      Text.Render(renderer, IntPtr.Zero, "Multi-Ball Active!", "Green", ScreenSize.Width - 200, ScreenSize.Height - 200, 12);
    }
    else
    {
      string comboMessage = $"Combo Progress: {consecutiveBlocksDestroyed}/{perkThreshold}";
      Text.Render(renderer, IntPtr.Zero, comboMessage, "White", ScreenSize.Width - 200, ScreenSize.Height - 220, 12);
    }
  }
}