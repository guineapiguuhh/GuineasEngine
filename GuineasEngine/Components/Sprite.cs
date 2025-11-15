using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public class Sprite : Node
{
    protected Texture2D Texture;

    public float Width => Texture.Width;
    public float Height => Texture.Height;

    public Sprite(Texture2D texture) : this("Sprite", texture) {}
    public Sprite(string name, Texture2D texture) : base(name)
    {
        Texture = texture;
    }

    public void CenterOrigin()
    {
        Origin = new Vector2(Texture.Width, Texture.Height) / 2f;
    }

    public override void Draw()
    {
        DrawTexture(
            Texture,
            Vector2.Zero,
            new Rectangle(0, 0, Texture.Width, Texture.Height),
            0f,
            Vector2.Zero,
            Vector2.Zero,
            SpriteEffects.None,
            0f
        );
        base.Draw();
    }
}