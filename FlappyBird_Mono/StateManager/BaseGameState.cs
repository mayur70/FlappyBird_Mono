using FlappyBird_Mono;
using Microsoft.Xna.Framework;
using System;

namespace StateManager
{
    public class BaseGameState : GameState
    {
        protected static Random random = new Random();
        protected GameMain GameRef;
        public BaseGameState(Game game) : base(game)
        {
            GameRef = (GameMain)game;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public virtual void Reset()
        {

        }
    }
}
