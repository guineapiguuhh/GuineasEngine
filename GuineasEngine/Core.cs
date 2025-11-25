using GuineasEngine.Components;
using GuineasEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Core : Game
{
    public static Core Instance { get; private set; }

    public static new GameWindow Window { get; private set; }

    public static GraphicsDeviceManager Graphics { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }

    public static new ContentManager Content { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }

    public static MouseData Mouse { get; private set; }
    public static KeyboardData Keyboard { get; private set; }
    public static GamePadData[] GamePads { get; private set; }

    public static int Width
    {
        get => Graphics.PreferredBackBufferWidth;
        set => Graphics.PreferredBackBufferWidth = value;
    }

    public static int Height
    {
        get => Graphics.PreferredBackBufferHeight;
        set => Graphics.PreferredBackBufferHeight = value;
    }

    public static bool IsFullScreen
    {
        get => Graphics.IsFullScreen;
        set => Graphics.IsFullScreen = value;
    }

    public static bool IsResizable
    {
        get => Window.AllowUserResizing;
        set => Window.AllowUserResizing = value;
    }

    public static bool IsBorderless
    {
        get => Window.IsBorderless;
        set => Window.IsBorderless = value;
    }

    public static Random Random { get; private set; }

    public static Color BackgroundColor { get; set; } = Color.Black;

    public static float DeltaTime { get; private set; } = 0f;

    static Scene CurrentScene { get; set; }
    static Scene NextScene { get; set; }

    public static Scene Scene 
    { 
        get => CurrentScene;
        set
        {
            if (value is null) throw new NullReferenceException();
            NextScene = value;
            DoTransition = true;
        }
    }

    static void SetScene(Scene scene) 
    {
        CurrentScene.Dispose();
        GC.Collect();

        CurrentScene = scene;
        CurrentScene.Load();
    }

    static Transition _inTransition { get; set; }
    public static Transition InTransition
    { 
        get => _inTransition;
        set
        {
            if (_inTransition == value) return;
            _inTransition?.Unload();
            _inTransition = value;
            _inTransition.Load();
            _inTransition.OnStop += (_, _) => {
                SetScene(NextScene);
                NextScene = null;
                OutTransition?.Play();
            };
        } 
    }

    static Transition _outTransition { get; set; }
    public static Transition OutTransition
    { 
        get => _outTransition;
        set
        {
            if (_outTransition == value) return;
            _outTransition?.Unload();
            _outTransition = value;
            _outTransition.Load();
            _outTransition.Reverse = true;
        } 
    }

    static bool DoTransition { get; set; } = false;

    static void StartSceneTransition()
    {
        DoTransition = false;
        if (InTransition is null)
        {
            SetScene(NextScene);
            NextScene = null;
            OutTransition?.Play();
            return;
        }
        InTransition?.Play();
    }
    
    public static void SetFPSLimit(double max)
    {
        Instance.IsFixedTimeStep = max > 0;
        if (Instance.IsFixedTimeStep) Instance.TargetElapsedTime = TimeSpan.FromSeconds(1 / max);
    }

    public Core(string title, Scene initScene, string contentDirectory, int width, int height, bool isFullScreen)
    {
        if (Instance is not null)
        {
            throw new InvalidOperationException("Can't initialize the Core more than once.");
        }
        Instance = this;

        Window = base.Window;
        Window.Title = title;

        Graphics = new GraphicsDeviceManager(this);
        Width = width;
        Height = height;
        IsFullScreen = isFullScreen;
        IsResizable = false;
        IsBorderless = false;
        IsMouseVisible = true;
        SetFPSLimit(60);

        Content = base.Content;
        Content.RootDirectory = contentDirectory;

        CurrentScene = initScene;
        CurrentScene.Load();
    }

    protected override void Initialize()
    {
        base.Initialize();

        GraphicsDevice = base.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        Mouse = new MouseData();
        Keyboard = new KeyboardData();
        GamePads = [
            new GamePadData(PlayerIndex.One),
            new GamePadData(PlayerIndex.Two),
            new GamePadData(PlayerIndex.Three),
            new GamePadData(PlayerIndex.Four),
        ];

        Random = new Random();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (IsActive)
        {
            Mouse.Update(DeltaTime);
            Keyboard.Update(DeltaTime);
            for (int i = 0; i < GamePads.Length; i++)
                GamePads[i].Update(DeltaTime);
        }

        InTransition?.Update(DeltaTime);
        OutTransition?.Update(DeltaTime);
        if (DoTransition) StartSceneTransition();
        Scene.Update(DeltaTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(BackgroundColor);
        SpriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            DepthStencilState.None,
            RasterizerState.CullCounterClockwise,
            null,
            Matrix.Identity
        );
        Scene.Draw(SpriteBatch);
        InTransition?.Draw(SpriteBatch);
        OutTransition?.Draw(SpriteBatch);
        SpriteBatch.End();
    }
}