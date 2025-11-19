using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GuineasEngine.Input;

public enum MouseButtons
{
    Left,
    Middle,
    Right,
    X1,
    X2
}

public class MouseData : Components.IUpdateable
{
    protected MouseState PreviousState;
    protected MouseState State;

    public Point Position
    {
        get => State.Position;
        set => Mouse.SetPosition(value.X, value.Y);
    }

    private MouseCursor cursor;
    public MouseCursor Cursor
    {
        get => cursor;
        set => Mouse.SetCursor(cursor = value);
    }

    public float ScrollWheel => State.ScrollWheelValue;
    public float HorizontalScrollWheel => State.HorizontalScrollWheelValue;

    public void Update(float deltaTime)
    {
        PreviousState = State;
        State = Mouse.GetState();
    }

    public bool IsPressed(MouseButtons button) => button switch
    {
        MouseButtons.Left => State.LeftButton == ButtonState.Pressed && PreviousState.LeftButton != ButtonState.Pressed,
        MouseButtons.Middle => State.MiddleButton == ButtonState.Pressed && PreviousState.MiddleButton != ButtonState.Pressed,
        MouseButtons.Right => State.RightButton == ButtonState.Pressed && PreviousState.RightButton != ButtonState.Pressed,
        MouseButtons.X1 => State.XButton1 == ButtonState.Pressed && PreviousState.XButton1 != ButtonState.Pressed,
        MouseButtons.X2 => State.XButton2 == ButtonState.Pressed && PreviousState.XButton2 != ButtonState.Pressed,
        _ => false,
    };

    public bool IsReleased(MouseButtons button) => button switch
    {
        MouseButtons.Left => State.LeftButton == ButtonState.Released && PreviousState.LeftButton != ButtonState.Released,
        MouseButtons.Middle => State.MiddleButton == ButtonState.Released && PreviousState.MiddleButton != ButtonState.Released,
        MouseButtons.Right => State.RightButton == ButtonState.Released && PreviousState.RightButton != ButtonState.Released,
        MouseButtons.X1 => State.XButton1 == ButtonState.Released && PreviousState.XButton1 != ButtonState.Released,
        MouseButtons.X2 => State.XButton2 == ButtonState.Released && PreviousState.XButton2 != ButtonState.Released,
        _ => false,
    };

    public bool IsDown(MouseButtons button) => button switch
    {
        MouseButtons.Left => State.LeftButton == ButtonState.Pressed,
        MouseButtons.Middle => State.MiddleButton == ButtonState.Pressed,
        MouseButtons.Right => State.RightButton == ButtonState.Pressed,
        MouseButtons.X1 => State.XButton1 == ButtonState.Pressed,
        MouseButtons.X2 => State.XButton2 == ButtonState.Pressed,
        _ => false,
    };

    public bool IsUp(MouseButtons button) => button switch
    {
        MouseButtons.Left => State.LeftButton == ButtonState.Released,
        MouseButtons.Middle => State.MiddleButton == ButtonState.Released,
        MouseButtons.Right => State.RightButton == ButtonState.Released,
        MouseButtons.X1 => State.XButton1 == ButtonState.Released,
        MouseButtons.X2 => State.XButton2 == ButtonState.Released,
        _ => false,
    };
}