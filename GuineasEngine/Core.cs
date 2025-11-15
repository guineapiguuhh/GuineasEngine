using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Core : Game
{
    public static Core Instance { get; private set; }

    public static GraphicsDeviceManager Graphics { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }

    public static SpriteBatch SpriteBatch { get; private set; }

    public static float DeltaTime { get; private set; } = 0f;

    public Core()
    {
        if (Instance is not null)
        {
            throw new Exception();
        }
        Instance = this;

        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = @"Content";
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Initialize()
    {
        base.Initialize();
        GraphicsDevice = base.GraphicsDevice;
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}