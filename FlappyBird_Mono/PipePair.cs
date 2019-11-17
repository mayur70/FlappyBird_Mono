using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird_Mono
{
    public class PipePair
    {
        private float x;
        private float y;
        public bool remove;
        private Pipe upper;
        private Pipe lower;

        public PipePair(float y)
        {
            x = GameMain.VIRTUAL_WIDTH + 32;
            this.y = y;
            remove = false;

            upper = new Pipe(PipeOrientation.Top, this.y);
            lower = new Pipe(PipeOrientation.Bottom, this.y + GameMain.PIPE_HEIGHT + GameMain.GAP_HEIGHT);
        }

        public void Update(float delta)
        {
            if (x > -GameMain.PIPE_WIDTH)
            {
                x -= GameMain.PIPE_SPEED * delta;
                upper.X = x;
                lower.X = x;
            }
            else
            {
                remove = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            upper.Draw(spriteBatch);
            lower.Draw(spriteBatch);
        }
    }
}
