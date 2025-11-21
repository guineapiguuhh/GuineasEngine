using System.Collections.ObjectModel;
using System.Text;
using GuineasEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Graphics;

public class Font
{
    public readonly SpriteFont SpriteFont;
    public ReadOnlyCollection<char> Characters => SpriteFont.Characters;
    
    public char? DefaultCharacter
    {
        get => SpriteFont.DefaultCharacter;
        set => SpriteFont.DefaultCharacter = value;
    }

    public SpriteFont.Glyph[] Glyphs => SpriteFont.Glyphs;

    public int LineSpacing
    {
        get => SpriteFont.LineSpacing;
        set => SpriteFont.LineSpacing = value;
    }
    
    public float Spacing
    {
        get => SpriteFont.Spacing;
        set => SpriteFont.Spacing = value;
    }

    public Texture2D Texture => SpriteFont.Texture;

    public Font(SpriteFont spriteFont)
    {
        SpriteFont = spriteFont;
    }

    public Dictionary<char, SpriteFont.Glyph> GetGlyphs() => SpriteFont.GetGlyphs();
    
    public Vector2 MeasureString(string content) => SpriteFont.MeasureString(content);
    public Vector2 MeasureString(StringBuilder content) => SpriteFont.MeasureString(content);

    public void Draw(
        SpriteBatch spriteBatch,
        StringBuilder content,
        HorizontalAlign horizontalAlign,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    ) => Draw(spriteBatch, content.ToString(), horizontalAlign, position, color, angle, origin, scale, effects, layerDepth);
    
    public void Draw(
        SpriteBatch spriteBatch,
        StringBuilder content,
        HorizontalAlign horizontalAlign,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    ) => Draw(spriteBatch, content.ToString(), horizontalAlign, position, color, angle, origin, scale, effects, layerDepth);

    public void Draw(
        SpriteBatch spriteBatch,
        string content,
        HorizontalAlign horizontalAlign,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    ) => Draw(spriteBatch, content, horizontalAlign, position, color, angle, origin, new Vector2(scale), effects, layerDepth);

    public void Draw(
        SpriteBatch spriteBatch,
        string content,
        HorizontalAlign horizontalAlign,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    )
    {
        var parts = content.Split(["\n", "\r", "\r\n"], StringSplitOptions.None);

        var size = MeasureString(content);

        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            var sx = MeasureString(part).X;
            spriteBatch.DrawString(
                SpriteFont,
                part,
                // PUTA QUE PARIU QUE MERDA É ESSA
                // o que eu fiz....
                Vector2.Transform(position,
                    Matrix.CreateTranslation(-position.X, -position.Y, 0f)
                    * Matrix.CreateTranslation((size.X - sx) * ((float)horizontalAlign / 2f), LineSpacing * i, 0f)
                    * Matrix.CreateScale(scale.X, scale.Y, 1f)
                    * Matrix.CreateRotationZ(angle)
                    * Matrix.CreateTranslation(position.X, position.Y, 0f)
                ),
                color,
                angle,
                origin,
                scale,
                effects,
                layerDepth
            );
        }
    }
}