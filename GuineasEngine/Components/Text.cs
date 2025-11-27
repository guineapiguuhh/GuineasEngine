using GuineasEngine.Graphics;
using GuineasEngine.Utils;
using GuineasEngine.Utils.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public class Text : Component
{
    public string Content = string.Empty;

    protected Font Font;
    public HorizontalAlign Align = HorizontalAlign.Left;

    public float Width { get; set; } = 0f;
    public float Height { get; set ; } = 0f;

    public Vector2 Position = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public Vector2 Origin = Vector2.Zero;
    public float Angle = 0f;

    public Color Color { get; set; } = Color.White;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    public void LoadFont(SpriteFont spriteFont) => LoadFont(new Font(spriteFont)); 
    public void LoadFont(Font font)
    {
        Font = font;
        UpdateSize();
    }

    public void CenterOrigin()
    {
        var size = Font.MeasureString(Content);
        Origin = new Vector2(size.X, size.Y) / 2f;
    }

    public void UpdateSize()
    {
        var size = Font.MeasureString(Content);
        Width = size.X;
        Height = size.Y;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Font?.Draw(
            spriteBatch,
            Content,
            Align,
            Entity.Position 
                + MathExtension.RotationTransformation(Position * Entity.Scale, Entity.Angle),
            Color,
            Entity.Angle + Angle,
            Origin,
            Entity.Scale * Scale,
            Effects,
            0f
        );
    }
}