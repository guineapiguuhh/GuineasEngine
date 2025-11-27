using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Component : IUpdateable, IDrawable
{
    public Entity Entity { get; internal set; }

    public bool IsActive { get; set; } = true;
    public bool IsVisible { get; set; } = true;

    public virtual void Ready() {}

    public virtual void Removed() {}

    public virtual void Update(float deltaTime) {}

    public virtual void Draw(SpriteBatch spriteBatch) {}

    public Component Clone()
    {
        var component = MemberwiseClone() as Component;
        component.Entity = null;
        return component;
    }

    public override string ToString()
    {
        return $"{GetType().Name}({Entity})";
    }
}