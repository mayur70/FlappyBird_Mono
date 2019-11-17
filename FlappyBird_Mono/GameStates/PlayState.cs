using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StateManager;
using System.Collections.Generic;

namespace FlappyBird_Mono.GameStates
{
    public interface IPlayState : IGameState
    {

    }
    public class PlayState : BaseGameState, IPlayState
    {

        public const int GAP_HEIGHT = 90;
        public const int PIPE_HEIGHT = 288;
        public const int PIPE_WIDTH = 70;
        public const int PIPE_SPEED = 60;

        public const int GRAVITY = 20;
        public const int JUMP_HEIGHT = -5;
        
        public static Texture2D pipe;

        private Bird bird;

        private List<PipePair> pipePairs;

        private float lastY;

        private float spawnTimer;

        private int score;


        public static SpriteFont smallFont;
        public static SpriteFont mediumFont;
        public static SpriteFont flappyFont;


        public PlayState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IPlayState), this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            pipePairs = new List<PipePair>();
            pipe = Game.Content.Load<Texture2D>("pipe");
            bird = new Bird(Game.Content.Load<Texture2D>("bird"));
            smallFont = Game.Content.Load<SpriteFont>("smallFont");
            mediumFont = Game.Content.Load<SpriteFont>("mediumFont");
            flappyFont = Game.Content.Load<SpriteFont>("flappyFont");
            
            Reset();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            spawnTimer += delta;
            if (spawnTimer > 2)
            {
                float y = MathHelper.Max(-PIPE_HEIGHT + 10,
                        MathHelper.Min(lastY + random.Next(-20, 20), GameMain.VIRTUAL_HEIGHT - 90 - PIPE_HEIGHT));
                lastY = y;
                spawnTimer = 0;
                pipePairs.Add(new PipePair(y));
            }

            bird.Update(delta);

            List<PipePair> pipePairsToRemove = new List<PipePair>();
            foreach (PipePair pipePair in pipePairs)
            {
                pipePair.Update(delta);

                if (!pipePair.scored)
                {
                    if(pipePair.X + PIPE_WIDTH < bird.X)
                    {
                        score++;
                        pipePair.scored = true;
                    }
                }


                if (bird.Collides(pipePair.Upper) || bird.Collides(pipePair.Lower))
                {
                    LoadScoreState();
                }

                if (pipePair.remove)
                {
                    pipePairsToRemove.Add(pipePair);
                }
            }
            foreach (PipePair pipePair in pipePairsToRemove)
            {
                pipePairs.Remove(pipePair);
            }

            if(bird.Y > GameMain.VIRTUAL_HEIGHT - 15)
            {
                LoadScoreState();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            foreach (PipePair pipePair in pipePairs)
            {
                pipePair.Draw(GameRef.SpriteBatch);
            }
            string scoreMsg = "Score: " + score;
            GameRef.SpriteBatch.DrawString(flappyFont, scoreMsg, new Vector2(8, 8), Color.White);

            bird.Draw(GameRef.SpriteBatch);

            base.Draw(gameTime);
        }

        private void LoadScoreState()
        {
            ScoreState scoreState = (ScoreState)GameRef.ScoreState;
            scoreState.Score = score;
            manager.ChangeState(scoreState);
        }

        public override void Reset()
        {
            if(bird != null)
                bird.Reset();
            if (pipePairs != null)
                pipePairs.Clear();
            spawnTimer = 0f;
            score = 0;
            lastY = -PIPE_HEIGHT + random.Next(80) + 20;
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
