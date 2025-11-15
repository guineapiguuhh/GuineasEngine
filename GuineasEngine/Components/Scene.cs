using Microsoft.Xna.Framework.Content;

namespace GuineasEngine;

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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Unload();
            Content?.Dispose();
        }
    }
}