public abstract class BaseScreen
{
  protected IntPtr renderer;
  public GameState NextState { get; set; }
  // Constructor que inicializa el renderer y establece el estado inicial
  protected BaseScreen(IntPtr renderer)
  {
    this.renderer = renderer;
    NextState = GameState.StartScreen;
  }

  // Métodos virtuales que pueden ser sobrescritos por las clases derivadas
  public virtual void Initialize() { }
  public virtual void Render() { }
  public virtual void HandleInput(object e) { }
  public virtual void Update() { }
}