using SDL2;

public class Block
{
  public SDL.SDL_Rect Rect { get; private set; }
  public bool IsVisible { get; set; } = true; // Para ocultar bloques después de la colisión
  private SDL.SDL_Color BlockColor { get; set; } // Color del bloque

  // Constructor que acepta un nombre de color opcional
  public Block(int x, int y, int width, int height, string colorName = "")
  {
    Rect = new SDL.SDL_Rect { x = x, y = y, w = width, h = height };

    // Usar color específico si se proporciona, de lo contrario, elegir un color aleatorio
    BlockColor = string.IsNullOrEmpty(colorName) ? Color.GetRandomColor(["white", "black", "red"]) : Color.GetColor(colorName);
  }

  public void Render(IntPtr renderer)
  {
    if (!IsVisible) return; // No renderizar si está oculto

    // Dibujar borde del bloque
    SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255); // Negro
    SDL.SDL_Rect borderRect = new SDL.SDL_Rect
    {
      x = Rect.x - 1,
      y = Rect.y - 1,
      w = Rect.w + 2,
      h = Rect.h + 2
    };
    SDL.SDL_RenderDrawRect(renderer, ref borderRect);

    // Dibujar bloque
    SDL.SDL_SetRenderDrawColor(renderer, BlockColor.r, BlockColor.g, BlockColor.b, BlockColor.a); // Usar el color del bloque
    var rect = Rect;
    SDL.SDL_RenderFillRect(renderer, ref rect);
  }
}