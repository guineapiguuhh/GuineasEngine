using System.Globalization;
using GuineasEngine.Utils.Math;
using Microsoft.Xna.Framework;

namespace GuineasEngine.Components.Colliders;

public class CircleCollider : Collider
{
    public int Radius = 0;

    public CircleCollider() : this("CircleCollider") {}
    public CircleCollider(string name) : base(name) {}

    public override bool Intersects(CircleCollider collider)
    {
        var squared = (Radius + collider.Radius) * (Radius + collider.Radius);
        var distanceSquared = Vector2.DistanceSquared(GlobalPosition, collider.GlobalPosition);
        return distanceSquared < squared;
    }

    public override bool Intersects(SquareCollider collider)
    {
        var v = VectorMath.Clamp(
            GlobalPosition,
            new Vector2(collider.Left, collider.Top),
            new Vector2(collider.Right, collider.Right)
        );

        var direction = GlobalPosition - v;
        var distanceSquared = direction.LengthSquared();

        return distanceSquared > 0f && distanceSquared < Radius * Radius;
    }

    public override bool Intersects(float x, float y, float width, float height)
    {
        var v = VectorMath.Clamp(
            GlobalPosition,
            new Vector2(x, y),
            new Vector2(x + width, y + height)
        );

        var direction = GlobalPosition - v;
        var distanceSquared = direction.LengthSquared();

        return distanceSquared > 0f && distanceSquared < Radius * Radius;
    }

    public override bool Intersects(float x, float y)
        => Intersects(x, y, 0f, 0f);

    public override bool Intersects(Point point)
        => Intersects(point.X, point.Y);
}