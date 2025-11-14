using GuineasEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Utils;

// TODO: implementar o sistema de desenhar texto

public static class Render
{
    public static SpriteBatch SpriteBatch { get; private set; }

    public static void Initialize()
    {
        SpriteBatch = new SpriteBatch(NewGame.GraphicsDevice);
    }

    public static void Begin()
    {
        SpriteBatch.Begin();
    }
    
    public static void End()
    {
        SpriteBatch.End();
    }

    public static void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle,
        Color color
    )
    {
        SpriteBatch.Draw(texture, destinationRectangle, color);
    }

    public static void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle sourceRectangle,
        Color color
    )
    {
        SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
    }

    public static void DrawTexture(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle sourceRectangle,
        Color color,
        float angle,
        Vector2 origin,
        SpriteEffects effects,
        float layerDepth
    )
    {
        SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, angle, origin, effects, layerDepth);
    }

    public static void DrawTexture(
        Texture2D texture,
        Vector2 position,
        Rectangle sourceRectangle,
        Color color,
        float angle,
        Vector2 origin,
        float scale,
        SpriteEffects effects,
        float layerDepth
    )
    {
        DrawTexture(texture, position, sourceRectangle, color, angle, origin, new Vector2(scale, scale), effects, layerDepth);
    }

    public static void DrawTexture(
        Texture2D texture,
        Vector2 position,
        Rectangle sourceRectangle,
        Color color,
        float angle,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    )
    {
        SpriteBatch.Draw(texture, position, sourceRectangle, color, angle, origin, scale, effects, layerDepth);
    }
}