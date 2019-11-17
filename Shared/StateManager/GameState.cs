using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace StateManager
{
    public interface IGameState
    {
        GameState Tag { get; }
    }
    public abstract partial class GameState : DrawableGameComponent, IGameState
    {
        protected readonly IStateManager manager;
        protected readonly ContentManager content;
        protected readonly List<GameComponent> childComponents;
        public GameState Tag { get; }
        public List<GameComponent> Components { get { return childComponents; } }
        public GameState(Game game) : base(game)
        {
            Tag = this;
            childComponents = new List<GameComponent>();
            content = Game.Content;

            manager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
                if (component.Enabled)
                    component.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (GameComponent component in childComponents)
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible)
                    ((DrawableGameComponent)component).Draw(gameTime);
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (manager.CurrentState == Tag)
                Show();
            else
                Hide();
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;

            foreach(GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;

            foreach(GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }
    }
}