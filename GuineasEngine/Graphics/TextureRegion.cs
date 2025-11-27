using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Graphics;

public class TextureRegion : IDisposable
{
    public readonly Texture2D Texture;
    public Rectangle SourceRectangle;

    public readonly int Width;
    public readonly int Height;

    public TextureRegion(Texture2D texture) 
        : this(texture, new Rectangle(0, 0, texture.Width, texture.Height)) {}

    public TextureRegion(Texture2D texture, int x, int y, int width, int height) 
        : this(texture, new Rectangle(x, y, width, height)) {}

    public TextureRegion(Texture2D texture, Rectangle source)
    {
        Texture = texture;
        Width = Texture.Width;
        Height = Texture.Height;
        SourceRectangle = source;
    }

    public void Draw(
        SpriteBatch spriteBatch,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    ) => Draw(spriteBatch, position, color, angle, origin, new Vector2(scale), effects, layerDepth);
    
    public void Draw(
        SpriteBatch spriteBatch,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    )
    {
        spriteBatch.Draw(
            Texture,
            position,
            SourceRectangle,
            color,
            angle,
            origin,
            scale,
            effects,
            layerDepth
        );
    }

    public static TextureRegion FromFile(GraphicsDevice graphics, string path)
    {
        return new TextureRegion(Texture2D.FromFile(graphics, path));
    }

    public static TextureRegion MakeGraphic(GraphicsDevice graphics, Color color, int width, int height)
    {
        var texture = new Texture2D(graphics, width, height);

        Color[] data = new Color[width * height];
        for (int pixel = 0; pixel < data.Length; pixel++) data[pixel] = color;
        texture.SetData(data);

        return new TextureRegion(texture);
    }

    public void Dispose()
    {
        Texture.Dispose();
        GC.SuppressFinalize(this);
    }
}