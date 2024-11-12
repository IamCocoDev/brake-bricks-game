public class ScoreManager
{
    private int score;
    private int consecutiveBlocks;
    private float multiplier;

    public int Score => score;

    public ScoreManager()
    {
        score = 0;
        consecutiveBlocks = 0;
        multiplier = 1.0f;
    }

    public void AddBlockDestroyed(bool isOriginal)
    {
        if (isOriginal)
        {
            consecutiveBlocks++;
        }

        if (consecutiveBlocks >= 6)
        {
            multiplier = 2.0f;
        }
        else if (consecutiveBlocks >= 3)
        {
            multiplier = 1.5f;
        }
        else
        {
            multiplier = 1.0f;
        }

        int baseScore = 10;

        if (isOriginal)
        {
            score += (int)(baseScore * multiplier);
        }

        if (!isOriginal)
        {
            score += baseScore;
        }
    }

    public void ResetCombo()
    {
        consecutiveBlocks = 0;
        multiplier = 1.0f;
    }

    public void Render(IntPtr renderer)
    {
        string scoreText = "Score: " + score.ToString();
        Text.Render(renderer, IntPtr.Zero, scoreText, "white", ScreenSize.Width - ScreenSize.Width + 75, ScreenSize.Height - 40, 12);
        RenderMultiplier(renderer);
    }

    public void RenderMultiplier(IntPtr renderer)
    {
        if (multiplier > 1.0f)
        {
            string multiplierText = "Multiplier: x" + multiplier.ToString("0.0");
            Text.Render(renderer, IntPtr.Zero, multiplierText, "yellow", ScreenSize.Width - ScreenSize.Width + 110, ScreenSize.Height - 60, 12);
        }
    }
}