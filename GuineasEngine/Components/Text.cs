using GuineasEngine.Graphics;
using GuineasEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public class Text : Component
{
    protected Font Font;

    public string Content = string.Empty;

    public HorizontalAlign Align = HorizontalAlign.Left;

    public float Width { get; set; } = 0f;
    public float Height { get; set ; } = 0f;

    public Vector2 Origin = Vector2.Zero;
    public Color Color { get; set; } = Color.White;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    public void LoadFont(SpriteFont spriteFont) => LoadFont(new Font(spriteFont)); 
    public void LoadFont(Font font)
    {
        Font = font;
        UpdateSize();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Font?.Draw(
            spriteBatch,
            Content,
            Align,
            Entity.Position,
            Color,
            Entity.Angle,
            Origin,
            Entity.Scale,
            Effects,
            0f
        );
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
}