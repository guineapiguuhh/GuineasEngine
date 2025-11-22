using GuineasEngine;
using GuineasEngine.Components;
using GuineasEngine.Graphics;
using Microsoft.Xna.Framework;

using GuineasEngine.Components.Colliders;
using GuineasEngine.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Colliders;

public class FirstScene : Scene
{
    private TransformNode EntitySelected;
    private Vector2 EntityDeltaPos = Vector2.Zero;

    private const int EntityQuantity = 1;
    private const int EntityWidth = 150;
    private const int EntityHeight = 150;

    private void CreateDraggableEntity(int id, TextureRegion texture)
    {
        var entity = new Sprite($"Entity_{id}");
        entity.Position = new Vector2(Core.Width, Core.Height) / 2f;
        entity.LoadTexture(texture);
        entity.CenterOrigin();
        AddChild(entity);

        var collider = new CircleCollider();
        //collider.Position -= entity.Origin;
        collider.Radius = EntityWidth;
        entity.AddChild(collider);
    }

    public override void Load()
    {
        var texture = TextureRegion.MakeGraphic(
            Core.GraphicsDevice, 
            Color.White, 
            EntityWidth, 
            EntityHeight
        );

        for (int i = 0; i < EntityQuantity; i++)
        {
            CreateDraggableEntity(i + 1, texture);
        }
    }

    public override void Update(float deltaTime)
    {
        var mousePositionVector = Core.Mouse.Position.ToVector2();
        for (int i = 0; i < EntityQuantity; i++)
        {
            if (GetChild(i) is not TransformNode child 
                || child.GetChild(0) is not CircleCollider collider) 
                continue;

            if (collider.Intersects(Core.Mouse.Position) 
                && Core.Mouse.IsPressed(MouseButtons.Left)
                && EntitySelected is null)
            {
                EntitySelected = child;
                EntityDeltaPos = child.Position - mousePositionVector;
                Core.Mouse.SetCursor(MouseCursor.SizeAll);
                break;
            }
            else if (Core.Mouse.IsReleased(MouseButtons.Left) && EntitySelected is not null)
            {
                EntitySelected = null;
                Core.Mouse.SetCursor(MouseCursor.Arrow);
            }
        }

        if (EntitySelected is not null)
        {
            EntitySelected.Position = EntityDeltaPos + mousePositionVector;
        }

        base.Update(deltaTime);
    }
}

public class Game1() : Core("Colliders", new FirstScene(), 800, 600, false);