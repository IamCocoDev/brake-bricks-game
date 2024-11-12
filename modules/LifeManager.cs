public class LifeManager
{
  private int lives;
  public int Lives => lives;

  public LifeManager(int initialLives = 3)
  {
    lives = initialLives;
  }

  public void LoseLife()
  {
    if (lives > 0)
    {
      lives--;
    }
  }
  public bool IsGameOver()
  {
    return lives <= 0;
  }

  public int GetLives()
  {
    return lives;
  }

  public void Render(IntPtr renderer)
  {
    Text.Render(renderer, IntPtr.Zero, $"Lives:{lives}", "white", ScreenSize.Width - 60, ScreenSize.Height - 40, 12);
  }
}