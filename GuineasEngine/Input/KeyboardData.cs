using Microsoft.Xna.Framework.Input;

namespace GuineasEngine.Input;

public class KeyboardData : Components.IUpdateable
{
    protected KeyboardState PreviousState;
    protected KeyboardState State;

    public void Update(float deltaTime)
    {
        PreviousState = State;
        State = Keyboard.GetState();
    }

    public bool IsPressed(Keys key) => State.IsKeyDown(key) && !PreviousState.IsKeyDown(key);

    public bool IsReleased(Keys key) => State.IsKeyUp(key) && !PreviousState.IsKeyUp(key);

    public bool IsDown(Keys key) => State.IsKeyDown(key);

    public bool IsUp(Keys key) => State.IsKeyUp(key);
}