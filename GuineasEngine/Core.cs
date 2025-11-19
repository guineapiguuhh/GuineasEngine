using GuineasEngine.Audio;
using GuineasEngine.Components;
using GuineasEngine.Input;
using GuineasEngine.Systems;
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

    public static InputManager Input { get; private set; }
    public static SceneManager SceneManager { get; private set; }
    public static Scene InitScene { get; private set; }

    public static double maxFPS { get; set; } = 0;
    public static double MaxFPS
    {
        get => maxFPS;
        set
        {
            maxFPS = value;
            Instance.IsFixedTimeStep = maxFPS > 0;
            Instance.TargetElapsedTime = TimeSpan.FromSeconds(1 / maxFPS);
        }
    }

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

    public static Color BackgroundColor { get; set; } = Color.CornflowerBlue;

    public Core(string title, Scene initScene, int width, int height, bool isFullScreen)
    {
        if (Instance is not null)
        {
            throw new Exception("Can't start more than one instance");
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
        MaxFPS = 60;

        Content = base.Content;
        Content.RootDirectory = @"Content";

        InitScene = initScene;
    }

    protected override void Initialize()
    {
        base.Initialize();

        GraphicsDevice = base.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        
        Input = new InputManager();
        SceneManager = new SceneManager();
        SceneManager.Switch(InitScene);
    }

    public static float DeltaTime { get; private set; } = 0f;

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        SceneManager.Update(DeltaTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(BackgroundColor);
        SceneManager.Draw();
    }
}