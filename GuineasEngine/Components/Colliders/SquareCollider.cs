namespace GuineasEngine.Components.Colliders;

public class SquareCollider : Collider
{
    public int Width = 0;
    public int Height = 0;

    public float Left => GlobalPosition.X;
    public float Right => GlobalPosition.X + Width;

    public float Top => GlobalPosition.Y;
    public float Bottom => GlobalPosition.Y + Height;

    public SquareCollider() : this("SquareCollider") {}
    public SquareCollider(string name) : base(name) {}

    public override bool Intersects(Collider collider) => false;
    public bool Intersects(SquareCollider collider)
    {
       if (collider.Left < Right && Left < collider.Right && collider.Top < Bottom)
        {
            return Top < collider.Bottom;
        }
        return false;
    }
}