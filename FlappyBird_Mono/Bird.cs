using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird_Mono
{
    public class Bird
    {
        private Texture2D image;
        private float width;
        private float height;
        private float x;
        private float y;
        private float dy;

        public Bird(Texture2D image)
        {
            this.image = image;
            width = this.image.Width;
            height = this.image.Height;

            x = GameMain.VIRTUAL_WIDTH / 2 - width / 2;
            y = GameMain.VIRTUAL_HEIGHT / 2 - height / 2;

            dy = 0f;
        }

        public void Update(float delta)
        {
            dy += GameMain.GRAVITY * delta;
            if (GameMain.input.WasPressed(Keys.Space))
            {
                dy = GameMain.JUMP_HEIGHT;
            }

            y += dy;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Vector2(x, y), Color.White);
        }
    }
}
