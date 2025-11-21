namespace GuineasEngine.Components.Colliders;

public abstract class Collider : TransformNode
{
    public Collider() : this("Collider") {}
    public Collider(string name) : base(name) {}

    public abstract bool Intersects(Collider collider);
}