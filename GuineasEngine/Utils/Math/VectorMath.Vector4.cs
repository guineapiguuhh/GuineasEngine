using Microsoft.Xna.Framework;

namespace GuineasEngine.Utils.Math;

public static partial class VectorMath
{
    public static Vector4 Min(Vector4 value, Vector4 min)
    {
        value.X = MathHelper.Min(value.X, min.X);
        value.Y = MathHelper.Min(value.Y, min.Y);
        value.Z = MathHelper.Min(value.Z, min.Z);
        value.W = MathHelper.Min(value.W, min.W);
        return value;
    }

    public static Vector4 Max(Vector4 value, Vector4 max)
    {
        value.X = MathHelper.Max(value.X, max.X);
        value.Y = MathHelper.Max(value.Y, max.Y);
        value.Z = MathHelper.Max(value.Z, max.Z);
        value.W = MathHelper.Max(value.W, max.W);
        return value;
    }

    public static Vector4 Clamp(Vector4 value, Vector4 min, Vector4 max)
    {
        value.X = MathHelper.Clamp(value.X, min.X, max.X);
        value.Y = MathHelper.Clamp(value.Y, min.Y, max.Y);
        value.Z = MathHelper.Clamp(value.Z, min.Z, max.Z);
        value.W = MathHelper.Clamp(value.W, min.W, max.W);
        return value;
    }

    public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
    {
        value1.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
        value1.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
        value1.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
        value1.W = MathHelper.SmoothStep(value1.W, value2.W, amount);
        return value1;
    }

    public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
    {
        value1.X = MathHelper.Lerp(value1.X, value2.X, amount);
        value1.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
        value1.Z = MathHelper.Lerp(value1.Z, value2.Z, amount);
        value1.W = MathHelper.Lerp(value1.W, value2.W, amount);
        return value1;
    }

    public static Vector4 LerpPrecise(Vector4 value1, Vector4 value2, float amount)
    {
        value1.X = MathHelper.LerpPrecise(value1.X, value2.X, amount);
        value1.Y = MathHelper.LerpPrecise(value1.Y, value2.Y, amount);
        value1.Z = MathHelper.LerpPrecise(value1.Z, value2.Z, amount);
        value1.W = MathHelper.LerpPrecise(value1.W, value2.W, amount);
        return value1;
    }
}