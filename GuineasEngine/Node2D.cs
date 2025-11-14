using GuineasEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Node2D : Node
{
    #region Global
    public Vector2 GlobalPosition = Vector2.Zero;
    public Vector2 GlobalScale = Vector2.One;
    public float GlobalAngle = 0f;
    #endregion

    public Vector2 Position = Vector2.Zero;
    public Vector2 Origin = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public float Angle = 0f;

    public Color Color = Color.White;
    public SpriteEffects Effect = SpriteEffects.None;

    public float LayerDepth = 0f;

    public Node2D()
    {
        Name = "Node2D";
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (GetParent() is Node2D node2D)
        {
            GlobalPosition = node2D.GlobalPosition + Position;
            GlobalScale = node2D.GlobalScale * Scale;
            GlobalAngle = node2D.GlobalAngle + Angle;
        }
    }
    
    #region Draw
    protected internal void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle
    )
    {
        Render.DrawTexture(texture, destinationRectangle, Color);
    }

    protected internal void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle sourceRectangle
    )
    {
        Render.DrawTexture(texture, destinationRectangle, sourceRectangle, Color);
    }

    protected internal void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle sourceRectangle,
        float angle,
        Vector2 origin,
        SpriteEffects effects,
        float layerDepth
    )
    {
        Render.DrawTexture(texture, destinationRectangle, sourceRectangle, Color, angle + GlobalAngle, origin, effects, layerDepth);
    }

    protected internal void DrawTexture(
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
        Render.DrawTexture(texture, position, sourceRectangle, Color, angle, origin, new Vector2(scale, scale), effects, layerDepth);
    }

    protected internal void DrawTexture(
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
        Render.DrawTexture(texture, position + GlobalPosition, sourceRectangle, Color, angle + GlobalAngle, origin, scale + GlobalScale, effects, layerDepth);
    }
    #endregion
}