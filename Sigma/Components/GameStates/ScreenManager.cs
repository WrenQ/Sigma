using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;



namespace Sigma.Components.GameStates
{
    /// <summary>
    /// Class in charge of managing screens. Contains a screen stack which is operated according to
    /// the current screen (its top) state.
    /// </summary>
    public class ScreenManager : GameComponent
    {
        #region Field region
        public event EventHandler OnScreenChange;

        Stack<GameScreen> screens = new Stack<GameScreen>();
        GameScreen nextScreen;
        #endregion
        #region Properties region
        public GameScreen ActiveScreen
        {
            get { return screens.Peek(); }
        }
        #endregion
        #region Constructor region
        public ScreenManager(Game game)
            : base(game)
        {
            
        }
        #endregion
        #region Monogame region 

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(ActiveScreen.State == ScreenState.Inactive)
            {
                if (!screens.Contains(nextScreen))
                {
                    AddScreen(nextScreen);
                    if (OnScreenChange != null)
                        OnScreenChange(this, null);
                }
                else
                {
                    RemoveActiveScreen();
                    ActiveScreen.State = ScreenState.Appearing;
                    if (OnScreenChange != null)
                        OnScreenChange(this, null);
                }
            }
            base.Update(gameTime);
        }

        #endregion
        #region Methods region
        /// <summary>
        /// Sets the current active screen as "leaving" so transition to black starts as soon as Update method is called. 
        /// </summary>
        public void PopScreen()
        {
            if (screens.Count > 0)
            {
                ActiveScreen.State = ScreenState.Leaving; //Inactive?
            }
        }

        /// <summary>
        /// Removes from the screen stack the current active screen, unscribing it from the OnScreenChange event handler.
        /// It also removes the screen from the game's components list.
        /// </summary>
        private void RemoveActiveScreen()
        {
            GameScreen State = screens.Peek();
            OnScreenChange -= State.ScreenChanged;
            Game.Components.Remove(State);
            screens.Pop();
        }


        /// <summary>
        /// Sets the new screen as the next screen to be added to the stack when its state is "active".
        /// Also sets the current screen as "leaving" so it fades to black.
        /// </summary>
        /// <param name="newScreen">The new screen to be added to the screen stack at its top.</param>
        public void PushScreen(GameScreen newScreen)
        {
            nextScreen = newScreen;
            ActiveScreen.State = ScreenState.Leaving; //Inactive?
        }


        /// <summary>
        /// Private method that manipulates the manager's screen stack. Sets the screen to be added as "appearing"
        /// so it fades from black to the proper screen content, adding it to the stack and also to the game's components list.
        /// Lastly it suscribes the new screen to the changing screens event handler.
        /// </summary>
        /// <param name="newScreen">The new screen to be added to the screen stack at its top.</param>
        private void AddScreen(GameScreen newScreen)
        {
            newScreen.State = ScreenState.Appearing;
            screens.Push(newScreen);
            Game.Components.Add(newScreen);
            OnScreenChange += newScreen.ScreenChanged;
        }

        /// <summary>
        /// Similar to "AddScreen method but this one first clears the screen stack so that the new screen desired to
        /// be added is first and only one. Useful when handling title and load screens (once they are used they can be disposed).
        /// </summary>
        /// <param name="newScreen">The new screen to be added to the screen stack.</param>
        public void ChangeScreens(GameScreen newScreen)
        {
            while (screens.Count > 0)
                RemoveActiveScreen();

            nextScreen = newScreen;
            nextScreen.State = ScreenState.Appearing;
            AddScreen(newScreen);

            if (OnScreenChange != null)
                OnScreenChange(this, null);
        }
        #endregion
    }

}