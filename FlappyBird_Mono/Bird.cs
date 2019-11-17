using FlappyBird_Mono.GameStates;
using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private SoundEffect jumpSound;

        public float X { get { return x; } }
        public float Y { get { return y; } }

        public Bird(Texture2D image, SoundEffect jumpSound)
        {
            this.image = image;
            this.jumpSound = jumpSound;
            width = this.image.Width;
            height = this.image.Height;

            Reset();
        }

        public void Update(float delta)
        {
            dy += PlayState.GRAVITY * delta;
            if (InputHandler.IsKeyJustPressed(Keys.Space))
            {
                jumpSound.Play();
                dy = PlayState.JUMP_HEIGHT;
            }

            y += dy;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Vector2(x, y), Color.White);
        }

        public bool Collides(Pipe pipe)
        {
            if ((x + 2 + width - 4) >= pipe.X && x + 2 <= pipe.X + PlayState.PIPE_WIDTH)
            {
                if ((y + 2 + height - 4) >= pipe.Y && y + 2 <= pipe.Y + PlayState.PIPE_HEIGHT)
                {
                    return true;
                }
            }
            return false;
        }
        public void Reset()
        {
            x = GameMain.VIRTUAL_WIDTH / 2 - width / 2;
            y = GameMain.VIRTUAL_HEIGHT / 2 - height / 2;

            dy = 0f;
        }
    }
}
