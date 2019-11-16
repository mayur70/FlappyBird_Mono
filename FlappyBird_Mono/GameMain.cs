using IndependentResolutionRendering;
using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        public static Random random = new Random();

        private Texture2D background;
        private float backgroundScroll = 0f;
        
        private Texture2D ground;
        private float groundScroll = 0f;

        private Texture2D pipe;

        private const int BACKGROUND_SCROLL_SPEED = 30;
        private const int GROUND_SCROLL_SPEED = 60;

        private const int BACKGROUND_LOOPING_POINT = 413;

        public static IInputHandler input;

        private Bird bird;

        private List<Pipe> pipes;

        private float spawnTimer = 0f;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Resolution.Init(ref graphics);
            Resolution.SetVirtualResolution(VIRTUAL_WIDTH, VIRTUAL_HEIGHT);
            Resolution.SetResolution(WINDOW_WIDTH, WINDOW_HEIGHT, false);


            input = new InputHandler(this);
            Components.Add((IGameComponent)input);

            Window.ClientSizeChanged += new EventHandler<EventArgs>(Resize);
        }


        protected override void Initialize()
        {

            Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.ApplyChanges();
            
            Window.Title = "Flappy Bird";

            spawnTimer = 0f;
            pipes = new List<Pipe>();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            background = Content.Load<Texture2D>("background");
            ground = Content.Load<Texture2D>("ground");
            pipe = Content.Load<Texture2D>("pipe");


            bird = new Bird(Content.Load<Texture2D>("bird"));
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

            spawnTimer += delta;
            if (spawnTimer > 2)
            {
                spawnTimer = 0;
                pipes.Add(new Pipe(pipe));
            }

            bird.Update(delta);

            List<Pipe> pipesToRemove = new List<Pipe>();
            foreach (Pipe pipe in pipes)
            {
                pipe.Update(delta);
                if (pipe.X < -pipe.Width)
                {
                    pipesToRemove.Add(pipe);
                }
            }
            foreach (Pipe pipe in pipesToRemove)
            {
                pipes.Remove(pipe);
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Resolution.BeginDraw();
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp, 
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise, 
                null,
                Resolution.getTransformationMatrix());

            spriteBatch.Draw(background, new Vector2(-backgroundScroll, 0), Color.White);

            foreach (Pipe pipe in pipes)
            {
                pipe.Draw(spriteBatch);
            }

            spriteBatch.Draw(ground, new Vector2(-groundScroll, VIRTUAL_HEIGHT - 16), Color.White);

            bird.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void Resize(object sender, EventArgs e)
        {
            Resolution.SetVirtualResolution(VIRTUAL_WIDTH, VIRTUAL_HEIGHT);
        }
    }
}
