using System.Runtime.CompilerServices;
using GuineasEngine.Graphics;
using GuineasEngine.Utils.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public class Sprite : Component
{
    protected TextureRegion Texture;
    public Rectangle SourceRectangle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Texture.SourceRectangle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Texture.SourceRectangle = value;
    }

    public float Width { get; set; } = 0f;
    public float Height { get; set; } = 0f;

    public Vector2 Position = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public Vector2 Origin = Vector2.Zero;
    public float Angle = 0f;

    public Color Color { get; set; } = Color.White;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

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
        Width = Texture.Width * Entity.Scale.X * Scale.X;
        Height = Texture.Height * Entity.Scale.Y * Scale.Y;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Texture?.Draw(
            spriteBatch,
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