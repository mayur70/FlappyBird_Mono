using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputManager
{
    public interface IInputHandler
    {

        KeyboardState KeyboardState { get; }
        GamePadState GamePadState { get; }

        bool WasPressed(Keys space);
    }

    public class InputHandler : GameComponent, IInputHandler
    {

        private static KeyboardState keyboardState;
        private static GamePadState gamepadState;
        public KeyboardState KeyboardState => keyboardState;
        public GamePadState GamePadState => gamepadState;

        public InputHandler(Game game) : base(game)
        {
            game.Services.AddService(typeof(IInputHandler), this);
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            gamepadState = GamePad.GetState(PlayerIndex.One);

            base.Update(gameTime);
        }

        public bool WasPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }
    }
}
