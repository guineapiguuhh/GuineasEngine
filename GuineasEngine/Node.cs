using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Node : Components.IUpdateable, Components.IDrawable
{
    public string Name = "Node";

    public Node Parent { get; internal set; }

    protected List<Node> Children { get; set; }

    #region Global
    public Vector2 GlobalPosition
    {
        get
        {
            if (Parent is not null)
            {
                return ApplyPoint(Parent.GlobalPosition + Position, Parent.GlobalAngle);
            }
            return Position;
        }
    }

    public Vector2 GlobalScale
    {
        get
        {
            if (Parent is not null)
            {
                return Parent.GlobalScale * Scale;
            }
            return Scale;
        }
    }

    public float GlobalAngle
    {
        get
        {
            if (Parent is not null)
            {
                return Parent.Angle + Angle;
            }
            return Angle;
        }
    }
    #endregion

    public Vector2 Position = Vector2.Zero;
    public Vector2 Origin = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public float Angle = 0f;

    public Color Color = Color.White;
    public SpriteEffects Effect = SpriteEffects.None;

    public float LayerDepth = 0f;

    public Node()
    {
        Children = [];
    }

    public void AddChild(Node child)
    {
        child.Parent = this;
        Children.Add(child);
    }

    public void InsertChild(int index, Node child)
    {
        child.Parent = this;
        Children.Insert(index, child);
    }

    public void RemoveChild(Node child)
    {
        child.Parent = null;
        Children.Remove(child);
    }

    public void ForEach(Action<Node> action)
    {
        for (int i = 0; i < Children.Count; i++)
        {
            action(Children[i]);
        }
    }

    private Vector2 ApplyPoint(Vector2 value, float angle)
    {
        return new Vector2();
    }

    public virtual void Update(float deltaTime)
    {
        ForEach((child) => child.Update(deltaTime));
    }

    public virtual void Draw()
    {
        ForEach((child) => child.Draw());
    }
    
    public override string ToString()
    {
        return Name;
    }

    #region Draw
    protected void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle
    )
    {
        Core.SpriteBatch.Draw(texture, destinationRectangle, Color);
    }

    protected void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle sourceRectangle
    )
    {
        Core.SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color);
    }

    protected void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle sourceRectangle,
        float angle,
        Vector2 origin,
        SpriteEffects effects,
        float layerDepth
    )
    {
        Core.SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color, angle + GlobalAngle, origin, effects, layerDepth);
    }

    protected void DrawTexture(
        Texture2D texture,
        Vector2 position,
        Rectangle sourceRectangle,
        float angle,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    )
    {
        Core.SpriteBatch.Draw(texture, position, sourceRectangle, Color, angle, origin, new Vector2(scale, scale), effects, layerDepth);
    }

    protected void DrawTexture(
        Texture2D texture,
        Vector2 position,
        Rectangle sourceRectangle,
        float angle,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    )
    {
        Core.SpriteBatch.Draw(texture, position + GlobalPosition, sourceRectangle, Color, angle + GlobalAngle, origin, scale + GlobalScale, effects, layerDepth);
    }
    #endregion
}