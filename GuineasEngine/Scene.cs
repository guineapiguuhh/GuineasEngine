using GuineasEngine.Utils.Internal;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public abstract class Scene : IUpdateable, IDrawable, IDisposable
{
    readonly internal EntityList Entities;
    protected ContentManager Content;

    public bool IsDisposed { get; private set; } = false;

    public Scene()
    {
        Entities = new EntityList(this);
    }
    ~Scene() => Dispose(false);

    public virtual void Load()
    {
        Content = new ContentManager(Core.Content.ServiceProvider);
        Content.RootDirectory = Core.Content.RootDirectory;
    }

    public virtual void Unload()
    {
        Entities.Clear();
        Content.Unload();
    }

    public int? IndexOf(Entity entity) => Entities.IndexOf(entity);

    public Entity Get(int index) => Entities[index];

    public void Add(Entity entity) => Entities.Add(entity);
    
    public void Insert(int index, Entity entity) => Entities.Insert(index, entity);

    public void Remove(Entity entity) => Entities.Remove(entity);

    public virtual void Update(float deltaTime) => Entities.Update(deltaTime);

    public virtual void Draw(SpriteBatch spriteBatch) => Entities.Draw(spriteBatch);

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Unload();
            Content?.Dispose();
        }
        IsDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}