public abstract class BaseScreen
{
  protected IntPtr renderer;
  public GameState NextState { get; set; }
  protected BaseScreen(IntPtr renderer)
  {
    this.renderer = renderer;
    NextState = GameState.StartScreen;
  }

  public virtual void Initialize() { }
  public virtual void Render() { }
  public virtual void HandleInput(object e) { }
  public virtual void Update() { }
}