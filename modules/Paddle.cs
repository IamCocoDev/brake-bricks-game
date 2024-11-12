using SDL2;

public class Paddle
{
  private SDL.SDL_Rect rect;
  private float speed;
  private float currentSpeed;
  private IntPtr renderer;

  public SDL.SDL_Rect Rect => rect;
  public float CurrentSpeed => currentSpeed;
  public Paddle(IntPtr renderer, int x, int y, int width = 100, int height = 20, float speed = 5f)
  {
    this.renderer = renderer;
    this.speed = speed;
    rect = new SDL.SDL_Rect { x = x, y = y, w = width, h = height };
    currentSpeed = 0f;
  }

  public void Render()
  {
    object color = Color.GetColor("white");
    SDL.SDL_SetRenderDrawColor(renderer, ((SDL.SDL_Color)color).r, ((SDL.SDL_Color)color).g, ((SDL.SDL_Color)color).b, ((SDL.SDL_Color)color).a);
    SDL.SDL_RenderFillRect(renderer, ref rect);
  }

  public void Update()
  {
    IntPtr keyState = SDL.SDL_GetKeyboardState(out int numKeys);
    byte[] keys = new byte[numKeys];
    System.Runtime.InteropServices.Marshal.Copy(keyState, keys, 0, numKeys);

    currentSpeed = 0f;

    if (keys[(int)SDL.SDL_Scancode.SDL_SCANCODE_LEFT] == 1)
    {
      rect.x -= (int)speed;
      currentSpeed = -speed;
      if (rect.x < 0) rect.x = 0;
    }

    if (keys[(int)SDL.SDL_Scancode.SDL_SCANCODE_RIGHT] == 1)
    {
      rect.x += (int)speed;
      currentSpeed = speed;
      if (rect.x + rect.w > ScreenSize.Width) rect.x = ScreenSize.Width - rect.w;
    }
  }

  public void ResetPosition(int x)
  {
    rect.x = x;
    currentSpeed = 0f;
  }
}