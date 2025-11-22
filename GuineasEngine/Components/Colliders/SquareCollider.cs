using GuineasEngine.Utils.Math;
using Microsoft.Xna.Framework;

namespace GuineasEngine.Components.Colliders;

public class SquareCollider : Collider
{
    public float Width = 0f;
    public float Height = 0f;

    public float Left => GlobalPosition.X;
    public float Right => GlobalPosition.X + Width;

    public Vector2 Center => GlobalPosition + new Vector2(Width, Height) / 2f;

    public float Top => GlobalPosition.Y;
    public float Bottom => GlobalPosition.Y + Height;

    public SquareCollider() : this("SquareCollider") {}
    public SquareCollider(string name) : base(name) {}

    public override bool Intersects(CircleCollider collider)
    {
        var v = VectorMath.Clamp(
            collider.GlobalPosition,
            new Vector2(Left, Top),
            new Vector2(Right, Right)
        );

        var direction = collider.GlobalPosition - v;
        var distanceSquared = direction.LengthSquared();

        return distanceSquared > 0f && distanceSquared < collider.Radius * collider.Radius;
    }

    public override bool Intersects(SquareCollider collider) 
        => Left < collider.Right && Right > collider.Left && Bottom > collider.Top && Top < collider.Bottom;

    public override bool Intersects(float x, float y, float width, float height)
        => Left < x + width && Right > x && Bottom > y && Top < y + height;

    public override bool Intersects(float x, float y)
        => x > Left && x < Right && y > Top && y < Bottom;

    public override bool Intersects(Point point)
    {
        return Intersects(point.X, point.Y);
    }
}