using Microsoft.Xna.Framework;

namespace GuineasEngine;

public class TransformNode : Node
{
    public TransformNode() : this("TransformNode") {}
    public TransformNode(string name) : base(name) {}

    #region Virtual methods
    public override void Update(float deltaTime)
    {
        UpdateGlobalTransform();
        base.Update(deltaTime);
    }
    #endregion

    #region Transform
    #region Global Transform
    public bool Independent { get; set; } = false;

    public Vector2 GlobalPosition { get; private set; }
    public Vector2 GlobalScale { get; private set; }
    public float GlobalAngle { get;  private set; }

    private void UpdateGlobalTransform()
    {
        var transformParent = Parent as TransformNode;
        if (transformParent is null || Independent)
        {
            GlobalAngle = Angle;
            GlobalScale = Scale;
            GlobalPosition = Position;
            return;
        }

        GlobalAngle = transformParent.GlobalAngle + Angle;
        GlobalScale = transformParent.GlobalScale * Scale;

        var angle = transformParent.GlobalAngle;

        var x = Position.X;
        var y = Position.Y;

        var sx = transformParent.GlobalScale.X;
        var sy = transformParent.GlobalScale.Y;

        var x2 = x * sx * float.Cos(angle) - y * sy * float.Sin(angle);
        var y2 = x * sx * float.Sin(angle) + y * sy * float.Cos(angle);

        GlobalPosition = transformParent.GlobalPosition + new Vector2(x2, y2);
    }
    #endregion
    public Vector2 Position = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public float Angle { get; set; } = 0f;

    public void LookAt(Vector2 value)
    {
        var dx = Position.X - value.X;
        var dy = Position.Y - value.Y;

        Angle = float.Atan2(dy, dx);
    }
    #endregion
}