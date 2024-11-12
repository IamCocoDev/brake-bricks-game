using SDL2;

public class Block
{
  public SDL.SDL_Rect Rect { get; private set; }
  public bool IsVisible { get; set; } = true;
  private SDL.SDL_Color BlockColor { get; set; }


  public Block(int x, int y, int width, int height, string colorName = "")
  {
    Rect = new SDL.SDL_Rect { x = x, y = y, w = width, h = height };


    BlockColor = string.IsNullOrEmpty(colorName) ? Color.GetRandomColor(["white", "black", "red"]) : Color.GetColor(colorName);
  }

  public void Render(IntPtr renderer)
  {
    if (!IsVisible) return;

    SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
    SDL.SDL_Rect borderRect = new SDL.SDL_Rect
    {
      x = Rect.x - 1,
      y = Rect.y - 1,
      w = Rect.w + 2,
      h = Rect.h + 2
    };
    SDL.SDL_RenderDrawRect(renderer, ref borderRect);


    SDL.SDL_SetRenderDrawColor(renderer, BlockColor.r, BlockColor.g, BlockColor.b, BlockColor.a);
    var rect = Rect;
    SDL.SDL_RenderFillRect(renderer, ref rect);
  }
}