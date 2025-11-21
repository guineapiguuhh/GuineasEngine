using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public interface ISprite
{
    public float Width { get; set; }
    public float Height { get; set; }

    public Color Color { get; set; }
    public SpriteEffects Effects { get; set; }

    public void CenterOrigin();

    public void UpdateSize();
}