using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird_Mono
{
    public class Pipe
    {
        private const int PIPE_SCROLL = -60;
        private Texture2D image;

        private float x;
        private float y;
        private float width;
        private float height;

        public float X { get { return x; } }
        public float Y { get { return y; } }
        public float Width { get { return width; } }
        public float Height { get { return height; } }
        public Pipe(Texture2D image)
        {
            this.image = image;
            x = GameMain.VIRTUAL_WIDTH;
            y = GameMain.random.Next(
                    GameMain.VIRTUAL_HEIGHT / 4, 
                    GameMain.VIRTUAL_HEIGHT - 10);
            width = this.image.Width;
            height = this.image.Height;
        }

        public void Update(float delta)
        {
            x += PIPE_SCROLL * delta;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Vector2(x, y), Color.White);
        }
    }
}
