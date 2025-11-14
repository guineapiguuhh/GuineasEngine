using GuineasEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public class Sprite : Node2D
{
    public Texture2D Texture;

    public Sprite(Texture2D texture)
    {
        Texture = texture;
    }

    public override void Draw()
    {
        base.Draw();
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
    }
}