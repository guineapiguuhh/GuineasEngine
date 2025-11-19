using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Graphics;

public class Font(SpriteFont spriteFont)
{
    public readonly SpriteFont SpriteFont = spriteFont;
    public ReadOnlyCollection<char> Characters => SpriteFont.Characters;
    public char? DefaultCharacter => SpriteFont.DefaultCharacter;
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

    public Dictionary<char, SpriteFont.Glyph> GetGlyphs() => SpriteFont.GetGlyphs();
    
    public Vector2 MeasureString(string content) => SpriteFont.MeasureString(content);
    public Vector2 MeasureString(StringBuilder content) => SpriteFont.MeasureString(content);

    public void Draw(
        SpriteBatch spriteBatch,
        StringBuilder content,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    ) => Draw(spriteBatch, content.ToString(), position, color, angle, origin, scale, effects, layerDepth);
    
    public void Draw(
        SpriteBatch spriteBatch,
        string content,
        Vector2 position,
        Color color,
        float angle,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    )
    {
        spriteBatch.DrawString(
            SpriteFont,
            content,
            position,
            color,
            angle,
            origin,
            scale,
            effects,
            layerDepth,
            false
        );
    }
}