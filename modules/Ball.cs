using SDL2;

public class Ball
{
  private SDL.SDL_Rect rect;
  private int speedX;
  private int speedY;
  private IntPtr renderer;
  private bool inPlay;
  private bool isOriginal;
  private SDL.SDL_Color color;
  private LifeManager lifeManager;

  public SDL.SDL_Rect Rect => rect;
  public bool InPlay => inPlay;
  public bool IsOriginal => isOriginal;

  public Ball(IntPtr renderer, int x, int y, bool isOriginal, LifeManager lifeManager, int width = 10, int height = 10, int speedX = 5, int speedY = -5)
  {
    this.renderer = renderer;
    this.lifeManager = lifeManager;
    this.speedX = speedX;
    this.speedY = speedY;
    this.isOriginal = isOriginal;
    rect = new SDL.SDL_Rect { x = x, y = y, w = width, h = height };
    inPlay = false;
    color = isOriginal ? new SDL.SDL_Color { r = 255, g = 0, b = 0, a = 255 } : new SDL.SDL_Color { r = 128, g = 128, b = 128, a = 255 };
  }

  public void Render()
  {
    SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
    SDL.SDL_RenderFillRect(renderer, ref rect);
  }

  public void Update(Paddle paddle)
  {
    if (!inPlay)
    {
      rect.x = paddle.Rect.x + (paddle.Rect.w / 2) - (rect.w / 2);
      rect.y = paddle.Rect.y - rect.h;
    }
    else
    {
      rect.x += speedX;
      rect.y += speedY;

      // Colisiones con paredes
      if (rect.x <= 0 || rect.x + rect.w >= ScreenSize.Width)
      {
        speedX = -speedX;
      }
      if (rect.y <= 0)
      {
        speedY = -speedY;
      }

      // Detectar si la bola salió de la pantalla
      if (rect.y > ScreenSize.Height)
      {
        inPlay = false;
        if (isOriginal)
        {
          lifeManager?.LoseLife();
          Console.WriteLine("Vida perdida. Vidas restantes: " + lifeManager?.GetLives());
        }
      }
    }
  }

  public void Launch(Paddle paddle)
  {
    if (!inPlay)
    {
      inPlay = true;
      IntPtr keyState = SDL.SDL_GetKeyboardState(out int numKeys);
      byte[] keys = new byte[numKeys];
      System.Runtime.InteropServices.Marshal.Copy(keyState, keys, 0, numKeys);

      if (keys[(int)SDL.SDL_Scancode.SDL_SCANCODE_LEFT] == 1)
      {
        speedX = -5;
      }
      else if (keys[(int)SDL.SDL_Scancode.SDL_SCANCODE_RIGHT] == 1)
      {
        speedX = 5;
      }
      else
      {
        int paddleCenter = paddle.Rect.x + (paddle.Rect.w / 2);
        int ballCenter = rect.x + (rect.w / 2);
        int offsetFromCenter = ballCenter - paddleCenter;
        speedX = offsetFromCenter / 10;
      }

      speedY = -5;
    }
  }

  public void ReverseVerticalDirection()
  {
    speedY = -speedY;
  }

  public bool HandleCollisionWithPaddle(Paddle paddle)
  {
    if (rect.y + rect.h >= paddle.Rect.y && rect.y <= paddle.Rect.y + paddle.Rect.h &&
        rect.x + rect.w >= paddle.Rect.x && rect.x <= paddle.Rect.x + paddle.Rect.w)
    {
      speedY = -speedY;
      int paddleCenter = paddle.Rect.x + (paddle.Rect.w / 2);
      int ballCenter = rect.x + (rect.w / 2);
      speedX = (ballCenter - paddleCenter) / 5;
      return true;
    }
    return false;
  }
}