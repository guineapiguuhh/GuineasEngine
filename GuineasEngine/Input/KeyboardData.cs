using Microsoft.Xna.Framework.Input;

namespace GuineasEngine.Input;

public class KeyboardData : IUpdateable
{
    protected KeyboardState PreviousState;
    protected KeyboardState State;

    public bool CapsLock => State.CapsLock;
    public bool NumLock => State.NumLock;

    public void Update(float deltaTime)
    {
        PreviousState = State;
        State = Keyboard.GetState();
    }

    public int GetPressedKeysCount() => State.GetPressedKeyCount();

    public void GetPressedKeys(Keys[] keys) => State.GetPressedKeys(keys);

    public Keys[] GetPressedKeys() => State.GetPressedKeys();

    public bool IsPressed(Keys key) => State.IsKeyDown(key) && !PreviousState.IsKeyDown(key);

    public bool IsReleased(Keys key) => State.IsKeyUp(key) && !PreviousState.IsKeyUp(key);

    public bool IsDown(Keys key) => State.IsKeyDown(key);

    public bool IsUp(Keys key) => State.IsKeyUp(key);
}