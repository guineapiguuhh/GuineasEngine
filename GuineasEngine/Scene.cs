using GuineasEngine.Utils.Collections;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public abstract class Scene : IUpdateable, IDrawable, IDisposable
{
    protected ContentManager Content;
    internal FastList<Entity> Entities;

    public Scene()
    {
        Entities = new FastList<Entity>();
    }
    ~Scene() => Dispose(false);

    public virtual void Load()
    {
        Content = new ContentManager(Core.Content.ServiceProvider);
        Content.RootDirectory = Core.Content.RootDirectory;
    }

    public virtual void Unload()
    {
        Content.Unload();
        Entities.Clear();
    }

    protected int? IndexOf(Entity entity)
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            if (Entities[i] == entity) return i;
        }
        return null;
    }

    protected void Add(Entity entity) => Insert(Entities.Count, entity);
    
    protected void Insert(int index, Entity entity)
    {
        Entities.Insert(index, entity);
        entity.QueueComponents();
        entity.Scene = this;
    }

    protected void Remove(Entity entity)
    {
        Entities.Remove(entity);
        entity.Scene = null;
        entity.ClearComponents();
    }

    public virtual void Update(float deltaTime)
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            Entities[i].Update(deltaTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            Entities[i].Draw(spriteBatch);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Unload();
            Content?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}