using Microsoft.Xna.Framework;

namespace GuineasEngine.Utils.Math;

public static partial class VectorMath
{
    public static Vector3 Min(Vector3 value, Vector3 min)
    {
        value.X = MathHelper.Min(value.X, min.X);
        value.Y = MathHelper.Min(value.Y, min.Y);
        value.Z = MathHelper.Min(value.Z, min.Z);
        return value;
    }

    public static Vector3 Max(Vector3 value, Vector3 max)
    {
        value.X = MathHelper.Max(value.X, max.X);
        value.Y = MathHelper.Max(value.Y, max.Y);
        value.Z = MathHelper.Max(value.Z, max.Z);
        return value;
    }

    public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
    {
        value.X = MathHelper.Clamp(value.X, min.X, max.X);
        value.Y = MathHelper.Clamp(value.Y, min.Y, max.Y);
        value.Z = MathHelper.Clamp(value.Z, min.Z, max.Z);
        return value;
    }

    public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
    {
        value1.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
        value1.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
        value1.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
        return value1;
    }

    public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
    {
        value1.X = MathHelper.Lerp(value1.X, value2.X, amount);
        value1.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
        value1.Z = MathHelper.Lerp(value1.Z, value2.Z, amount);
        return value1;
    }

    public static Vector3 LerpPrecise(Vector3 value1, Vector3 value2, float amount)
    {
        value1.X = MathHelper.LerpPrecise(value1.X, value2.X, amount);
        value1.Y = MathHelper.LerpPrecise(value1.Y, value2.Y, amount);
        value1.Z = MathHelper.LerpPrecise(value1.Z, value2.Z, amount);
        return value1;
    }
}