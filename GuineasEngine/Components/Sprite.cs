using GuineasEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public class Sprite : TransformNode, ISprite
{
    protected TextureRegion Texture;

    public float Width { get; set; } = 0f;
    public float Height { get; set; } = 0f;

    public Vector2 Origin = Vector2.Zero;
    public Color Color { get; set; } = Color.White;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    public Sprite() : this("Sprite") {}
    public Sprite(string name) : base(name) {}

    public void LoadTexture(Texture2D texture) => LoadTexture(new TextureRegion(texture));
    public void LoadTexture(TextureRegion texture)
    {
        Texture = texture;
        UpdateSize();
    }

    public void CenterOrigin()
    {
        Origin = new Vector2(Texture.Width, Texture.Height) / 2f;
    }

    public void UpdateSize()
    {
        Width = Texture.Width * Scale.X;
        Height = Texture.Height * Scale.Y;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Texture?.Draw(
            spriteBatch,
            GlobalPosition,
            Color,
            GlobalAngle,
            Origin,
            GlobalScale,
            Effects,
            0f
        );
        base.Draw(spriteBatch);
    }
}