using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GuineasEngine.Input;

public class GamePadData : IUpdateable
{
    protected GamePadState PreviousState;
    protected GamePadState State;
    
    public GamePadThumbSticks ThumbSticks => State.ThumbSticks;
    public GamePadTriggers Triggers => State.Triggers;

    public bool IsConnected => State.IsConnected;
    public int PacketNumber => State.PacketNumber;
    public PlayerIndex Index;

    public GamePadData(int index) : this((PlayerIndex)index) {}
    public GamePadData(PlayerIndex index)
    {
        Index = index;
    }

    public void Update(float deltaTime)
    {
        PreviousState = State;
        State = GamePad.GetState(Index);
    }

    public void SetVibration(float leftMotor, float rightMotor)
        => GamePad.SetVibration(Index, leftMotor, rightMotor);
        
    public void SetVibration(float leftMotor, float rightMotor, float leftTrigger, float rightTrigger) 
        => GamePad.SetVibration(Index, leftMotor, rightMotor, leftTrigger, rightTrigger);

    public bool IsPressed(Buttons button) => State.IsButtonDown(button) && !PreviousState.IsButtonDown(button);
    
    public bool IsReleased(Buttons button) => State.IsButtonUp(button) && !PreviousState.IsButtonUp(button);
    
    public bool IsDown(Buttons button) => State.IsButtonDown(button);

    public bool IsUp(Buttons button) => State.IsButtonUp(button);
}