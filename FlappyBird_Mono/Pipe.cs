using FlappyBird_Mono.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird_Mono
{
    public enum PipeOrientation
    {
        Top,
        Bottom
    }
    public class Pipe
    {
        private Texture2D image;

        private float x;
        private readonly float y;
        private readonly float width;
        private readonly float height;
        private PipeOrientation orientation;

        public float X 
        { 
            get 
            { 
                return x; 
            }
            set
            {
                x = value;
            }
        }
        public float Y {
            get 
            {
                return y; 
            }
        }
        public float Width { get { return width; } }
        public float Height { get { return height; } }
        public Pipe(PipeOrientation orientation, float y)
        {
            image = PlayState.pipe;
            x = GameMain.VIRTUAL_WIDTH;
            this.y = y;
            width = image.Width;
            height = image.Height;
            this.orientation = orientation;
        }

        public void Update(float delta)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = orientation == PipeOrientation.Top ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(
                image, 
                new Vector2(x,y), 
                new Rectangle(0, 0, (int)width, (int)height), 
                Color.White, 
                0f, 
                Vector2.Zero,
                1f,
                spriteEffects, 
                0f);
        }
    }
}
