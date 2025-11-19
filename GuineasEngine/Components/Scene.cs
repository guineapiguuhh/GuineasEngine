using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GuineasEngine.Components;

public abstract class Scene : Node
{
    protected ContentManager Content;

    public Scene() : this("Scene") {}
    public Scene(string name) : base(name) {}
    ~Scene() => Dispose(false);

    public virtual void Load()
    {
        Content = new ContentManager(Core.Content.ServiceProvider);
        Content.RootDirectory = Core.Content.RootDirectory;
    }

    public virtual void Unload()
    {
        Content?.Unload();
    }

    public override void Draw()
    {
        Core.SpriteBatch.Begin();
        base.Draw();
        Core.SpriteBatch.End();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Unload();
            Content?.Dispose();
        }
    }
}