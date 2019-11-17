using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StateManager;

namespace Shared.GameStates
{
    public interface IScoreState : IGameState
    {

    }
    public class ScoreState : BaseGameState, IScoreState
    {
        public static SpriteFont mediumFont;
        public static SpriteFont flappyFont;
        public int Score { get; set; }
        private const string lostMsg = "Oof!, You Lost!";
        private const string restartMsg = "Press Enter to Play Again!";
        public ScoreState(Game game) : base(game)
        {

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
            Vector2 msgSize = flappyFont.MeasureString(lostMsg);
            GameRef.SpriteBatch.DrawString(flappyFont, lostMsg, new Vector2(GameMain.VIRTUAL_WIDTH / 2 - msgSize.X / 2, 64), Color.White);
            string scoreMsg = "Score: " + Score;
            msgSize = mediumFont.MeasureString(scoreMsg);
            GameRef.SpriteBatch.DrawString(mediumFont, scoreMsg, new Vector2(GameMain.VIRTUAL_WIDTH / 2 - msgSize.X / 2, 100), Color.White);
            
            msgSize = mediumFont.MeasureString(restartMsg);
            GameRef.SpriteBatch.DrawString(mediumFont, restartMsg, new Vector2(GameMain.VIRTUAL_WIDTH / 2 - msgSize.X / 2, 160), Color.White);

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
