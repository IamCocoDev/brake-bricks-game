using SDL2;

public class Paddle
{
  private SDL.SDL_Rect rect;
  private float speed;
  private IntPtr renderer;

  public SDL.SDL_Rect Rect => rect;

  public Paddle(IntPtr renderer, int x, int y, int width = 100, int height = 20, float speed = 5f)
  {
    this.renderer = renderer;
    this.speed = speed;
    rect = new SDL.SDL_Rect { x = x, y = y, w = width, h = height };
  }

  public void Render()
  {
    SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255); // Blanco
    SDL.SDL_RenderFillRect(renderer, ref rect);
  }

  public void Update()
  {
    IntPtr keyState = SDL.SDL_GetKeyboardState(out int numKeys);
    byte[] keys = new byte[numKeys];
    System.Runtime.InteropServices.Marshal.Copy(keyState, keys, 0, numKeys);

    if (keys[(int)SDL.SDL_Scancode.SDL_SCANCODE_LEFT] == 1)
    {
      rect.x -= (int)speed;
      if (rect.x < 0) rect.x = 0;
    }

    if (keys[(int)SDL.SDL_Scancode.SDL_SCANCODE_RIGHT] == 1)
    {
      rect.x += (int)speed;
      if (rect.x + rect.w > ScreenSize.Width) rect.x = ScreenSize.Width - rect.w;
    }
  }

  public void ResetPosition(int x)
  {
    rect.x = x;
  }
}