using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GuineasEngine.Utils.Internal;
using System.Runtime.CompilerServices;
using GuineasEngine.Components;

namespace GuineasEngine;

public class Entity : IUpdateable, IDrawable
{
    private static uint _availableID = 0;

    public string Name = string.Empty;
    public uint ID = 0;

    public Scene Scene { get; internal set; }
    readonly ComponentList Components;

    public Vector2 Position = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public float Angle = 0f;

    public Entity(string name)
    {
        Name = name;
        ID = _availableID++;
        Components = new ComponentList(this);
    }

    public void QueueComponents() => Components.QueueMembers();

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

    public virtual void Update(float deltaTime)
    {
        Components.Update(deltaTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Components.Draw(spriteBatch);
    }

    public override string ToString()
    {
        return $"{GetType().Name}({Name})";
    }
}