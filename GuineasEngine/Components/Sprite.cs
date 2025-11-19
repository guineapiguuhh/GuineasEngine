using GuineasEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public class Sprite : Node
{
    protected TextureRegion Texture;

    public float Width = 0f;
    public float Height = 0f;

    public Color Color = Color.White;
    public SpriteEffects Effects = SpriteEffects.None;

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

    public override void Draw()
    {
        Texture.Draw(
            Core.SpriteBatch,
            GlobalPosition,
            Color,
            GlobalAngle,
            Origin,
            GlobalScale,
            Effects,
            0f
        );
        base.Draw();
    }
}