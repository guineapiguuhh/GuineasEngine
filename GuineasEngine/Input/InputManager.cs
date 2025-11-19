using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GuineasEngine.Input;

public class InputManager : Components.IUpdateable
{
    protected readonly KeyboardData Keyboard;
    protected readonly MouseData Mouse;
    protected readonly GamePadData[] GamePads;

    public Point MousePosition
    {
        get => Mouse.Position;
        set => Mouse.Position = value;
    }

    public MouseCursor MouseCursor
    {
        get => Mouse.Cursor;
        set => Mouse.Cursor = value;
    }

    public InputManager()
    {
        Keyboard = new KeyboardData();
        Mouse = new MouseData();
        GamePads = [
            new GamePadData(PlayerIndex.One),
            new GamePadData(PlayerIndex.Two),
            new GamePadData(PlayerIndex.Three),
            new GamePadData(PlayerIndex.Four),
        ];
    }

    public void Update(float deltaTime)
    {
        Keyboard.Update(deltaTime); 
        Mouse.Update(deltaTime);
        for (int i = 0; i < GamePads.Length; i++)
            GamePads[i].Update(deltaTime);
    }

    public bool IsKeyPressed(Keys key) => Keyboard.IsPressed(key);
    public bool IsKeyReleased(Keys key) => Keyboard.IsReleased(key);
    public bool IsKeyDown(Keys key) => Keyboard.IsDown(key);
    public bool IsKeyUp(Keys key) => Keyboard.IsUp(key);

    public bool IsMousePressed(MouseButtons button) => Mouse.IsPressed(button);
    public bool IsMouseReleased(MouseButtons button) => Mouse.IsReleased(button);
    public bool IsMouseDown(MouseButtons button) => Mouse.IsDown(button);
    public bool IsMouseUp(MouseButtons button) => Mouse.IsUp(button);

    public void SetVibration(PlayerIndex index, float leftMotor, float rightMotor) => GamePads[(int)index].SetVibration(leftMotor, rightMotor);
    public void SetVibration(int index, float leftMotor, float rightMotor) => GamePads[index].SetVibration(leftMotor, rightMotor);
    public void SetVibration(PlayerIndex index, float leftMotor, float rightMotor, float leftTrigger, float rightTrigger) 
        => GamePads[(int)index].SetVibration(leftMotor, rightMotor, leftTrigger, rightTrigger);
    public void SetVibration(int index, float leftMotor, float rightMotor, float leftTrigger, float rightTrigger) 
        => GamePads[index].SetVibration(leftMotor, rightMotor, leftTrigger, rightTrigger);

    public Vector2 GetLeftThumbStick(PlayerIndex index) => GamePads[(int)index].ThumbSticks.Left;
    public Vector2 GetLeftThumbStick(int index) => GamePads[index].ThumbSticks.Left;

    public Vector2 GetRightThumbStick(PlayerIndex index) => GamePads[(int)index].ThumbSticks.Right;
    public Vector2 GetRightThumbStick(int index) => GamePads[index].ThumbSticks.Right;

    public float GetLeftTrigger(PlayerIndex index) => GamePads[(int)index].Triggers.Left;
    public float GetLeftTrigger(int index) => GamePads[index].Triggers.Left;

    public float GetRightTrigger(PlayerIndex index) => GamePads[(int)index].Triggers.Right;
    public float GetRightTrigger(int index) => GamePads[index].Triggers.Right;

    public bool IsButtonPressed(PlayerIndex index, Buttons button) => GamePads[(int)index].IsPressed(button);
    public bool IsButtonPressed(int index, Buttons button) => GamePads[index].IsPressed(button);

    public bool IsButtonReleased(PlayerIndex index, Buttons button) => GamePads[(int)index].IsReleased(button);
    public bool IsButtonReleased(int index, Buttons button) => GamePads[index].IsReleased(button);

    public bool IsButtonDown(PlayerIndex index, Buttons button) => GamePads[(int)index].IsDown(button);
    public bool IsButtonDown(int index, Buttons button) => GamePads[index].IsDown(button);

    public bool IsButtonUp(PlayerIndex index, Buttons button) => GamePads[(int)index].IsUp(button);
    public bool IsButtonUp(int index, Buttons button) => GamePads[index].IsUp(button);
}