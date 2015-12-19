using System;
using System.Collections.Generic;


using Microsoft.Xna.Framework;

namespace Sigma.Components.GameStates
{
    public class GameState : DrawableGameComponent
    {
        #region Fields and properties

        List<GameComponent> childComponents;
        GameState tag;
        protected GameStateManager StateManager;

        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        public GameState Tag
        {
            get { return tag; }
        }

        #endregion

        #region Constructor
        public GameState(Game game, GameStateManager stateManager):base(game)
        {
            StateManager = stateManager;
            childComponents = new List<GameComponent>();
            tag = this;
        }
        #endregion

        #region Update and Draw methods

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(GameComponent component in childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawableComponent;
            foreach(GameComponent component in childComponents)
            {
                if(component is DrawableGameComponent)
                {
                    drawableComponent = component as DrawableGameComponent;
                    if (drawableComponent.Visible)
                        drawableComponent.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }

        #endregion

        #region GameState Methods

        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (StateManager.CurrentState == Tag) Show();
            else Hide();
        }

        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;
            foreach(GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }

        #endregion
    }
}
