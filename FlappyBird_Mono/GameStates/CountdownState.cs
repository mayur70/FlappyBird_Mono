using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StateManager;

namespace FlappyBird_Mono.GameStates
{
    public interface ICountdownState : IGameState
    {

    }
    public class CountdownState : BaseGameState, ICountdownState
    {
        private const float COUNTDOWN_TIME = 0.75f;
        private int count;
        private float timer;

        public static SpriteFont hugeFont;

        public CountdownState(Game game) : base(game)
        {
            Reset();
        }

        protected override void LoadContent()
        {

            hugeFont = Game.Content.Load<SpriteFont>("hugeFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(timer > COUNTDOWN_TIME)
            {
                timer %= COUNTDOWN_TIME;
                count--;

                if(count == 0)
                {
                    PlayState playState = (PlayState)GameRef.PlayState;
                    playState.Reset();
                    manager.ChangeState(playState);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            string counter = count.ToString();
            Vector2 msgSize = hugeFont.MeasureString(counter);
            GameRef.SpriteBatch.DrawString(hugeFont, counter, new Vector2(GameMain.VIRTUAL_WIDTH / 2 - msgSize.X / 2, 120), Color.White);
            base.Draw(gameTime);
        }

        public override void Reset()
        {
            count = 3;
            timer = 0;
            base.Reset();
        }
    }
}
