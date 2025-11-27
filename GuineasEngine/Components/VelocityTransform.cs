using Microsoft.Xna.Framework;

namespace GuineasEngine.Components;

public class VelocityTransform : Component
{
    public Vector2 Velocity = Vector2.Zero;
    public float AngularVelocity = 0f;

    public Vector2 Acceleration = Vector2.Zero;
    public float AngularAcceleration = 0f;

    public override void Update(float deltaTime)
    {
        Velocity += Acceleration;
        AngularVelocity += AngularAcceleration;
        
        Entity.Position += Velocity * deltaTime;
        Entity.Angle += AngularVelocity * deltaTime;
    }
}