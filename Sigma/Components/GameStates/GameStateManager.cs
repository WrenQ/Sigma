using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Sigma.Components.GameStates
{
    public class GameStateManager : GameComponent
    {
        public event EventHandler OnStateChange;

        #region Fields and Properties

        Stack<GameState> gameStates = new Stack<GameState>();
        const int startDrawOrder = 2000,
                  drawOrderIncrement = 100;
        int drawOrder;

        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }

        #endregion

        #region Consructors

        public GameStateManager(Game game) : base(game)
        {
            drawOrder = startDrawOrder;
        }

        #endregion

        #region Monogame Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #endregion

        #region GameStateManager Methods

        public void PopState()
        {
            if(gameStates.Count > 0)
            {
                RemoveState();
                drawOrder -= drawOrderIncrement;

                if (OnStateChange != null)
                    OnStateChange(this, null);
            }
        }

        public void RemoveState()
        {
            GameState state = gameStates.Peek();
            OnStateChange -= state.StateChange;
            Game.Components.Remove(state);
            gameStates.Pop();
        }

        public void PushState(GameState state)
        {
            drawOrder += drawOrderIncrement;
            state.DrawOrder = drawOrder;
            AddState(state);
            if (OnStateChange != null)
                OnStateChange(this, null);

        }

        private void AddState(GameState state)
        {
            gameStates.Push(state);
            Game.Components.Add(state);
            OnStateChange += state.StateChange;
        }

        private void ChangeState(GameState state)
        {
            while(gameStates.Count > 0)
            {
                RemoveState();
            }

            state.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;

            AddState(state);

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        #endregion


    }
}