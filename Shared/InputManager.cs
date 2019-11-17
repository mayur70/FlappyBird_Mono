using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace InputManager
{
    public class InputHandler : GameComponent
    {

        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;
        private static GamePadState currentGamepadState;
        private static GamePadState previousGamepadState;
        private static TouchCollection currentTouchCollection;
        public static KeyboardState KeyboardState => currentKeyboardState;
        public static GamePadState GamePadState => currentGamepadState;

        public InputHandler(Game game) : base(game)
        {
            TouchPanel.EnableMouseTouchPoint = true;
        }

        public override void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            previousGamepadState = currentGamepadState;

            if(TouchPanel.GetCapabilities().IsConnected)
                currentTouchCollection = TouchPanel.GetState();
            currentKeyboardState = Keyboard.GetState();
            currentGamepadState = GamePad.GetState(PlayerIndex.One);

            base.Update(gameTime);
        }

        public static void FlushInput()
        {
            currentKeyboardState = previousKeyboardState;
            currentGamepadState = previousGamepadState;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool IsScreenTouched()
        {
            if (TouchPanel.GetCapabilities().IsConnected)
            { 
                bool pressed = false;
                foreach (TouchLocation touchLocation in currentTouchCollection)
                {
                    if (touchLocation.State == TouchLocationState.Pressed)
                    {
                        pressed = true;
                        break;
                    }
                }
                return pressed;
            }
            return false;
        }

        public static bool IsKeyJustPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }
    }
}
