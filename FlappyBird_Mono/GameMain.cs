using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScalingClever;

namespace FlappyBird_Mono
{
    public class GameMain : Game
    {
        private const int WINDOW_WIDTH = 1280;
        private const int WINDOW_HEIGHT = 720;

        public const int VIRTUAL_WIDTH = 512;
        public const int VIRTUAL_HEIGHT = 288;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private Texture2D background;
        private float backgroundScroll = 0f;
        
        private Texture2D ground;
        private float groundScroll = 0f;

        private const int BACKGROUND_SCROLL_SPEED = 30;
        private const int GROUND_SCROLL_SPEED = 60;

        private const int BACKGROUND_LOOPING_POINT = 413;

        private IInputHandler input;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            input = new InputHandler(this);
            Components.Add((IGameComponent)input);

        }


        protected override void Initialize()
        {

            Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.ApplyChanges();
            ResolutionScaling.Initialize(this, new Point(VIRTUAL_WIDTH, VIRTUAL_HEIGHT));

            Window.Title = "Flappy Bird";
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ResolutionScaling.LoadContent(this, new Point(VIRTUAL_WIDTH, VIRTUAL_HEIGHT));

            background = Content.Load<Texture2D>("background");
            ground = Content.Load<Texture2D>("ground");
        }

        protected override bool BeginDraw()
        {
            ResolutionScaling.BeginDraw(this);
            return base.BeginDraw();
        }

        protected override void EndDraw()
        {
            ResolutionScaling.EndDraw(this, spriteBatch);
            base.EndDraw();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (input.GamePadState.Buttons.Back == ButtonState.Pressed || input.KeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            backgroundScroll = (backgroundScroll + BACKGROUND_SCROLL_SPEED * delta) % BACKGROUND_LOOPING_POINT;
            groundScroll = (groundScroll + GROUND_SCROLL_SPEED * delta) % VIRTUAL_WIDTH;
        
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(-backgroundScroll, 0), Color.White);
            spriteBatch.Draw(ground, new Vector2(-groundScroll, VIRTUAL_HEIGHT - 16), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
