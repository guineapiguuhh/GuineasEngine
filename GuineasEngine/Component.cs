using System.Runtime.InteropServices.Marshalling;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Component : IUpdateable, IDrawable
{
    public Entity Entity;

    public virtual void Ready() {}

    public virtual void Removed() {}

    public virtual void Update(float deltaTime) {}

    public virtual void Draw(SpriteBatch spriteBatch) {}

    public virtual Component Clone()
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