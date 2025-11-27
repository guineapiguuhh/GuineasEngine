using Microsoft.Xna.Framework;

namespace GuineasEngine.Utils.Math;

public static partial class MathExtension
{
    public static Vector2 RotationTransformation(Vector2 vec, float angle) => RotationTransformation(vec.X, vec.Y, angle);
    public static Vector2 RotationTransformation(float x, float y, float angle)
    {
        var x2 = x * float.Cos(angle) - y * float.Sin(angle);
        var y2 = x * float.Sin(angle) + y * float.Cos(angle);
        return new Vector2(x2, y2);
    }
}