using GuineasEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class NewGame : Game
{
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static float DeltaTime { get; private set; } = 0f;

    public NewGame()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = @"Content";
    }

    protected override void Initialize()
    {
        base.Initialize();
        GraphicsDevice = base.GraphicsDevice;
        Render.Initialize();
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