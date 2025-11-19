using Microsoft.Xna.Framework;

namespace GuineasEngine.Utils.Math;

public static partial class VectorMath
{
    public static Vector2 Min(Vector2 value, Vector2 min)
    {
        value.X = MathHelper.Min(value.X, min.X);
        value.Y = MathHelper.Min(value.Y, min.Y);
        return value;
    }

    public static Vector2 Max(Vector2 value, Vector2 max)
    {
        value.X = MathHelper.Max(value.X, max.X);
        value.Y = MathHelper.Max(value.Y, max.Y);
        return value;
    }

    public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
    {
        value.X = MathHelper.Clamp(value.X, min.X, max.X);
        value.Y = MathHelper.Clamp(value.Y, min.Y, max.Y);
        return value;
    }

    public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
    {
        value1.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
        value1.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
        return value1;
    }

    public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
    {
        value1.X = MathHelper.Lerp(value1.X, value2.X, amount);
        value1.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
        return value1;
    }

    public static Vector2 LerpPrecise(Vector2 value1, Vector2 value2, float amount)
    {
        value1.X = MathHelper.LerpPrecise(value1.X, value2.X, amount);
        value1.Y = MathHelper.LerpPrecise(value1.Y, value2.Y, amount);
        return value1;
    }
}