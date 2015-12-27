using System;
using System.Collections.Generic;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sigma.Components.GameStates
{
    public enum ScreenState
    {
        Appearing,
        Active,
        Leaving,
        Inactive
    }

    public class GameScreen : DrawableGameComponent
    {

        List<GameComponent> childComponents;
        GameScreen identifier;
        protected ScreenManager ScreenManager;
        float alpha = 1f;
        ScreenState state = ScreenState.Inactive;

        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        public GameScreen Identifier
        {
            get { return identifier; }
        }

        public float Alpha
        {
            get { return alpha; }
            set
            {
                if (value < 0f)
                    value = 0f;
                else if (value > 1f)
                    value = 1f;

                MathHelper.Clamp(value, 0f, 1f);
                alpha = value;
            }
        }

        public ScreenState State
        {
            get { return state; }
            set { state = value; }
        }

        public GameScreen(Game game, ScreenManager manager)
            : base(game)
        {
            childComponents = new List<GameComponent>();
            identifier = this;
            ScreenManager = manager;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(state == ScreenState.Leaving)
            {
                Alpha += 0.02f;
                if (Alpha == 1f)
                    state = ScreenState.Inactive;
            }

            if(state == ScreenState.Appearing)
            {
                Alpha -= 0.02f;
                if (Alpha == 0f)
                    state = ScreenState.Active;
            }

            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;
            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        internal protected virtual void ScreenChanged(object sender, EventArgs e)
        {
            if (ScreenManager.ActiveScreen == Identifier)
                ShowScreen();
            else
                HideScreen();
        }

        protected virtual void ShowScreen()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }

        }

        protected virtual void HideScreen()
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
    }
}

