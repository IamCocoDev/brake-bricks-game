public class LifeManager
{
  private int lives;
  public int Lives => lives;

  public LifeManager(int initialLives = 10)
  {
    lives = initialLives;
  }

  public void LoseLife()
  {
    if (lives > 0)
    {
      lives--;
      Console.WriteLine($"Life lost! Remaining lives: {lives}");
    }
    else
    {
      Console.WriteLine("No lives left!");
      // game state to game over
    }
  }
  public bool IsGameOver()
  {
    return lives < 0;
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