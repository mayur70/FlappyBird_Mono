using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StateManager;

namespace Shared.GameStates
{
    public interface ITitleScreenState: IGameState
    {

    }
    public class TitleScreenState : BaseGameState, ITitleScreenState
    {
        public static SpriteFont mediumFont;
        public static SpriteFont flappyFont;
        private const string titleMsg = "Flappy Bird";
        private const string continueMsg = "Press Enter";

        public TitleScreenState(Game game) : base(game)
        {
            game.Services.AddService(typeof(ITitleScreenState), this);
        }

        protected override void LoadContent()
        {
            mediumFont = Game.Content.Load<SpriteFont>("mediumFont");
            flappyFont = Game.Content.Load<SpriteFont>("flappyFont");
               
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter) || InputHandler.IsScreenTouched())
            {
                CountdownState countdownState = (CountdownState)GameRef.CountdownState;
                countdownState.Reset();
                manager.ChangeState(countdownState);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 msgSize = flappyFont.MeasureString(titleMsg);
            GameRef.SpriteBatch.DrawString(flappyFont, titleMsg, 
                new Vector2((GameMain.VIRTUAL_WIDTH / 2) - (msgSize.X / 2),64), Color.White);

            msgSize = mediumFont.MeasureString(continueMsg); 
            GameRef.SpriteBatch.DrawString(mediumFont, continueMsg,
                new Vector2((GameMain.VIRTUAL_WIDTH / 2) - (msgSize.X / 2), 100), Color.White);

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
