using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Sigma.Components.Input
{
    /// <summary>
    /// This class manages the user's input when using a compatible game pad.
    /// </summary>
    public class InputHandler : GameComponent
    {
        #region Field Region

        static GamePadState gamepadState;
        static GamePadState lastGamepadState;

        #endregion

        #region Property region

        public static GamePadState GamePadState
        {
            get { return gamepadState; }
        }

        public static GamePadState LastGamePadState
        {
            get { return lastGamepadState; }
        }

        #endregion

        #region Constructor region

        public InputHandler(Game game)
            : base(game)
        {
            gamepadState = GamePad.GetState(new PlayerIndex(), GamePadDeadZone.None);
        }

        #endregion

        #region Monogame methods

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            lastGamepadState = gamepadState;
            gamepadState = GamePad.GetState(new PlayerIndex(), GamePadDeadZone.None);
            base.Update(gameTime);
        }

        #endregion

        #region General Method Region

        /// <summary>
        /// Clears the last game pad state
        /// </summary>
        public static void Flush()
        {
            lastGamepadState = gamepadState;
        }

        #endregion

        #region Gamepad region

        /// <summary>
        /// Checks whether the desired button has been released by comparing its current and last state
        /// </summary>
        /// <param name="button">The button to be checked.</param>
        public static bool ButtonReleased(Buttons button)
        {
            return gamepadState.IsButtonUp(button) && lastGamepadState.IsButtonDown(button);
        }

        /// <summary>
        /// Checks whether the desired button has been pressed by comparing its current and last state
        /// </summary>
        /// <param name="button">The button to be checked.</param>
        public static bool ButtonPressed(Buttons button)
        {
            return gamepadState.IsButtonDown(button) && lastGamepadState.IsButtonUp(button);
        }

        /// <summary>
        /// Checks whether the desired button is being held.
        /// </summary>
        /// <param name="button">The button to be checked.</param>
        public static bool ButtonDown(Buttons button)
        {
            return gamepadState.IsButtonDown(button);
        }

        #endregion
    }
}
