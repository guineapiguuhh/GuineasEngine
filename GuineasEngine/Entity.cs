using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GuineasEngine.Utils.Internal;

namespace GuineasEngine;

public class Entity : IUpdateable, IDrawable, IDisposable
{
    private static uint _availableId = 0;

    public string Name = string.Empty;
    public uint ID = 0;

    public Scene Scene { get; internal set; }
    readonly ComponentList Components;

    public Vector2 Position = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public float Angle = 0f;

    public bool IsActive { get; set; } = true;
    public bool IsVisible { get; set; } = true;

    public bool IsDisposed { get; private set; } = false;

    public Entity(string name)
    {
        Name = name;
        ID = _availableId++;
        Components = new ComponentList(this);
    }

    public void ResolveComponents() => Components.ResolveRequests();

    public void ClearComponents() => Components.Clear();

    public T AddComponent<T>(T component)
        where T : Component
    {
        Components.Add(component);
        return component;
    }

    public T AddComponent<T>()
        where T : Component, new()
    {
        var component = new T();
        Components.Add(component);
        return component;
    }

    public bool RemoveComponent<T>()
        where T : Component 
    { 
        var component = GetComponent<T>();
        if (component is not null)
        {
            Components.Remove(component);
            return true;
        }
        return false;
    }

    public bool HasComponent<T>()
        where T : Component => Components.Has<T>();

    public T GetComponent<T>()
        where T : Component => Components.Get<T>();

    public virtual void Update(float deltaTime) => Components.Update(deltaTime);

    public virtual void Draw(SpriteBatch spriteBatch) => Components.Draw(spriteBatch);

    public override string ToString() => $"{GetType().Name}({Name})";

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed) return;

        if (disposing)
        {
            ClearComponents();
            Scene.Remove(this);
            Scene = null;
        }
        IsDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}