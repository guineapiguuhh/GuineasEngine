using Microsoft.Xna.Framework;

namespace GuineasEngine.Components.Colliders;

public class CircleCollider : Collider
{
    public int Radius = 0;

    public CircleCollider() : this("CircleCollider") {}
    public CircleCollider(string name) : base(name) {}

    public bool Intersects(CircleCollider collider)
    {
        var squared = (Radius + collider.Radius) * (Radius + collider.Radius);
        var distanceSquared = Vector2.DistanceSquared(GlobalPosition, collider.GlobalPosition);
        return distanceSquared < squared;
    }

    public override bool Intersects(Collider collider)
    {
        return false;
    }
}