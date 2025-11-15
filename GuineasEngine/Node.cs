using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Node : Components.IUpdateable, Components.IDrawable, IDisposable
{
    public string Name;
    public bool Independent = false;

    public Node Parent { get; internal set; }

    protected List<Node> Children { get; set; }
    public int ChildrenCount => Children.Count;

    #region Global
    public Vector2 GlobalPosition { get; private set; }
    public Vector2 GlobalScale { get; private set; }
    public float GlobalAngle { get; private set; }
    #endregion

    public Vector2 Position = Vector2.Zero;
    public Vector2 Origin = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public float Angle = 0f;

    public Color Color = Color.White;
    public SpriteEffects Effect = SpriteEffects.None;

    public float LayerDepth = 0f;
    
    public Node() : this("Node") {}
    public Node(string name)
    {
        Name = name;
        Children = [];
    }

    public void AddChild(Node child)
    {
        InsertChild(ChildrenCount, child);
    }

    public void InsertChild(int index, Node child)
    {
        child.Parent?.RemoveChild(child);
        if (child == this)
        {
            throw new Exception("You can't add him as your own child.");
        }

        child.Parent = this;
        Children.Insert(index, child);
    }

    public void RemoveChild(Node child)
    {
        if (Children.Contains(child)) return;

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

    public virtual void Update(float deltaTime)
    {
        CalcGlobalPosition();
        ForEach((child) => child.Update(deltaTime));
    }

    public virtual void Draw()
    {
        ForEach((child) => child.Draw());
    }

    private void CalcGlobalPosition()
    {
        if (Parent is null || Independent)
        {
            GlobalAngle = Angle;
            GlobalScale = Scale;
            GlobalPosition = Position;
            return;
        }

        GlobalAngle = Parent.GlobalAngle + Angle;
        GlobalScale = Parent.GlobalScale * Scale;

        var angle = Parent.GlobalAngle;

        var x = Position.X;
        var y = Position.Y;

        var sx = Parent.GlobalScale.X;
        var sy = Parent.GlobalScale.Y;

        var x2 = x * sx * float.Cos(angle) - y * sy * float.Sin(angle);
        var y2 = x * sx * float.Sin(angle) + y * sy * float.Cos(angle);

        GlobalPosition = Parent.GlobalPosition + new Vector2(x2, y2);
    }

    protected virtual void Dispose(bool disposing) {}

    public void Dispose()
    {
        Dispose();
        GC.SuppressFinalize(this);
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
        Core.SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color, angle + GlobalAngle, Origin + origin, effects, layerDepth);
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
        DrawTexture(texture, position, sourceRectangle, angle, origin, new Vector2(scale, scale), effects, layerDepth);
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
        Core.SpriteBatch.Draw(texture, position + GlobalPosition, sourceRectangle, Color, angle + GlobalAngle, Origin + origin, scale + GlobalScale, effects, layerDepth);
    }
    #endregion
}