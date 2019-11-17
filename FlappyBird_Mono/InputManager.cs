using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputManager
{
    public class InputHandler : GameComponent
    {

        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;
        private static GamePadState currentGamepadState;
        private static GamePadState previousGamepadState;
        public static KeyboardState KeyboardState => currentKeyboardState;
        public static GamePadState GamePadState => currentGamepadState;

        public InputHandler(Game game) : base(game)
        {

        }

        public override void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            previousGamepadState = currentGamepadState;

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
