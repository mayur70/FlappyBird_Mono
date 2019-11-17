using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird_Mono
{
    public class PipePair
    {
        private float x;
        private float y;
        public bool remove;

        public Pipe Upper { get; }
        public Pipe Lower { get; }

        public PipePair(float y)
        {
            x = GameMain.VIRTUAL_WIDTH + 32;
            this.y = y;
            remove = false;

            Upper = new Pipe(PipeOrientation.Top, this.y);
            Lower = new Pipe(PipeOrientation.Bottom, this.y + GameMain.PIPE_HEIGHT + GameMain.GAP_HEIGHT);
        }

        public void Update(float delta)
        {
            if (x > -GameMain.PIPE_WIDTH)
            {
                x -= GameMain.PIPE_SPEED * delta;
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
