using FlappyBird_Mono.GameStates;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird_Mono
{
    public class PipePair
    {
        private float x;
        private float y;
        public bool remove;
        public bool scored;

        public Pipe Upper { get; }
        public Pipe Lower { get; }
        public float X { get { return x; } }

        public PipePair(float y)
        {
            x = GameMain.VIRTUAL_WIDTH + 32;
            this.y = y;
            remove = false;

            scored = false;

            Upper = new Pipe(PipeOrientation.Top, this.y);
            Lower = new Pipe(PipeOrientation.Bottom, this.y + PlayState.PIPE_HEIGHT + PlayState.GAP_HEIGHT);
        }

        public void Update(float delta)
        {
            if (x > -PlayState.PIPE_WIDTH)
            {
                x -= PlayState.PIPE_SPEED * delta;
                Upper.X = x;
                Lower.X = x;
            }
            else
            {
                remove = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Upper.Draw(spriteBatch);
            Lower.Draw(spriteBatch);
        }
    }
}
