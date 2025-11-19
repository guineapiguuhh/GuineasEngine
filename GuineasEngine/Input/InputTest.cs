using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GuineasEngine.Input;

public class InputTest(InputManager input) : Components.IUpdateable
{
    private enum InputTestState
    {
        Keyboard,
        Mouse,
        GamePad
    }

    int CurSelected = 0;
    InputTestState State = InputTestState.Keyboard;

    bool GamePadVibration = false;

    public void Update(float deltaTime)
    {
        if (input.IsKeyPressed(Keys.Escape))
        {
            CurSelected += 1;
            if (CurSelected > 2) CurSelected = 0;
            State = (InputTestState)CurSelected;

            Console.WriteLine($"{State} ({CurSelected})");
        }

        #region Keyboard
        if (State == InputTestState.Keyboard)
        {
            if (input.IsKeyPressed(Keys.A))
            {
                Console.WriteLine("A is pressed!");
            }

            if (input.IsKeyReleased(Keys.W))
            {
                Console.WriteLine("W is released!");
            }

            if (input.IsKeyDown(Keys.S))
            {
                Console.WriteLine("S is down!");
            }

            if (input.IsKeyUp(Keys.D) && input.IsKeyDown(Keys.LeftShift))
            {
                Console.WriteLine("D is up!");
            }
        }
        #endregion
        #region Mouse
        if (State == InputTestState.Mouse)
        {
            if (input.IsMousePressed(MouseButtons.Left))
            {
                Console.WriteLine("Left button is pressed!");
            }

            if (input.IsMouseReleased(MouseButtons.Middle))
            {
                Console.WriteLine("Middle button is released!");
            }

            if (input.IsMouseDown(MouseButtons.Right))
            {
                Console.WriteLine("Right button is down!");
            }

            if (input.IsMouseUp(MouseButtons.X1) && input.IsMouseDown(MouseButtons.X2))
            {
                Console.WriteLine("Xbutton1 is up!");
            }
        }
        #endregion
        #region GamePad
        if (State == InputTestState.GamePad)
        {
            if (input.IsButtonPressed(0, Buttons.A))
            {
                Console.WriteLine("A is pressed!");
            }

            if (input.IsButtonReleased(0, Buttons.B))
            {
                Console.WriteLine("B is released!");
            }

            if (input.IsButtonDown(0, Buttons.X))
            {
                Console.WriteLine("X is down!");
            }

            if (input.IsButtonUp(0, Buttons.Y) && input.IsButtonDown(0, Buttons.LeftStick))
            {
                Console.WriteLine("Y is up!");
            }

            if (input.IsButtonPressed(0, Buttons.DPadUp))
            {
                GamePadVibration = !GamePadVibration;
                if (GamePadVibration) input.SetVibration(0, 0.5f, 0.5f);
                else input.SetVibration(0, 0f, 0f);
            }

            Vector2 leftThumbStick = input.GetLeftThumbStick(0);
            if (leftThumbStick != Vector2.Zero)
            {
                Console.WriteLine($"Left Thumb Stick: {leftThumbStick}");
            }

            Vector2 rightThumbStick = input.GetRightThumbStick(0);
            if (rightThumbStick != Vector2.Zero)
            {
                Console.WriteLine($"Right Thumb Stick: {rightThumbStick}");
            }

            float leftTrigger = input.GetLeftTrigger(0);
            if (leftTrigger != 0.0f)
            {
                Console.WriteLine($"Left Trigger: {leftTrigger}");
            }

            float rightTrigger = input.GetRightTrigger(0);
            if (rightTrigger != 0.0f)
            {
                Console.WriteLine($"Right Trigger: {rightTrigger}");
            }
        }
        #endregion
    }
}