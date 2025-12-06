using GuineasEngine.Utils.Collections;
using GuineasEngine.Utils.Internal;
using GuineasEngine.Utils;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public abstract class Scene : IUpdateable, IDrawable, IDisposable
{
    readonly internal EntityList Entities;
    protected ContentManager Content;

    public FastList<Timer> Timers;

    public bool IsDisposed { get; private set; } = false;

    public Scene()
    {
        Entities = new EntityList();
        Timers = new FastList<Timer>();
    }
    ~Scene() => Dispose(false);

    public void Initialize()
    {
        Load();
        Entities.ResolveRequests();
    }

    public virtual void Load()
    {
        Content = new ContentManager(Core.Content.ServiceProvider);
        Content.RootDirectory = Core.Content.RootDirectory;
    }

    public virtual void Unload()
    {
        Entities.Clear();
        Timers.Clear();
        Content.Unload();
    }

    public int IndexOf(Entity entity) => Entities.IndexOf(entity);

    public Entity Get(int index) => Entities[index];

    public void Add(Entity entity) => Insert(Entities.Count, entity);
    
    public void Insert(int index, Entity entity) 
    {
        entity.Scene = this;
        entity.ResolveComponents();
        Entities.Insert(index, entity);
    }

    public void Remove(Entity entity) 
    {
        if (entity.Scene != this) return;
        entity.Scene = null;
        Entities.Remove(entity);
    }

    public virtual void Update(float deltaTime) 
    {
        for (int i = 0; i < Timers.Count; i++)
        {
            Timers[i].Update(deltaTime);
        }
        Entities.Update(deltaTime);
    }

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