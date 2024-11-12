
using SDL2;

public enum GameState
{
    StartScreen,
    Playing,
    GameOver,
    Closing,
    Credits,
    Victory
}

public static class ScreenSize
{
    public const int Width = 640;
    public const int Height = 900;
}

class Program
{
    static void Main()
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"Error initializing SDL: {SDL.SDL_GetError()}");
            return;
        }

        IntPtr window = SDL.SDL_CreateWindow("Brake Bricks Game", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, ScreenSize.Width, ScreenSize.Height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
        IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        SDL_ttf.TTF_Init();
        GameState currentState = GameState.StartScreen;
        BaseScreen currentScreen = new StartScreen(renderer);
        currentScreen.Initialize();

        bool running = true;

        int finalScore = 0;

        while (running)
        {
            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    running = false;
                }

                currentScreen.HandleInput(e);

                if (currentScreen.NextState != currentState)
                {
                    finalScore = (currentScreen as GameScreen)?.GetCurrentScore() ?? 0;
                    currentState = currentScreen.NextState;

                    switch (currentState)
                    {
                        case GameState.StartScreen:
                            currentScreen = new StartScreen(renderer);
                            currentScreen.Initialize();
                            break;
                        case GameState.Playing:
                            currentScreen = new GameScreen(renderer);
                            currentScreen.Initialize();
                            break;
                        case GameState.GameOver:
                            currentScreen = new GameOverScreen(renderer, finalScore, false);
                            currentScreen.Initialize();
                            break;
                        case GameState.Closing:
                            running = false;
                            break;
                        case GameState.Victory:
                            currentScreen = new GameOverScreen(renderer, finalScore, true);
                            currentScreen.Initialize();
                            break;
                        case GameState.Credits:
                            currentScreen = new CreditScreen(renderer);
                            currentScreen.Initialize();
                            break;
                    }

                    currentScreen.NextState = currentState;
                }
            }

            currentScreen.Update();
            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
            SDL.SDL_RenderClear(renderer);
            currentScreen.Render();
            SDL.SDL_RenderPresent(renderer);
        }
        // Limpieza
        SDL_ttf.TTF_Quit();
        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
    }
}