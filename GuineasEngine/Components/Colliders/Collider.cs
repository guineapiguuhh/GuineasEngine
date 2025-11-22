using Microsoft.Xna.Framework;

namespace GuineasEngine.Components.Colliders;

public abstract class Collider : TransformNode
{
    public Collider() : this("Collider") {}
    public Collider(string name) : base(name) {}

    public abstract bool Intersects(CircleCollider collider);

    public abstract bool Intersects(SquareCollider collider);

    public abstract bool Intersects(float x, float y, float width, float height);

    public abstract bool Intersects(float x, float y);

    public abstract bool Intersects(Point point);
}