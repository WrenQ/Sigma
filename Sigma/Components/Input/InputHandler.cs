using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Sigma.Components.Input
{
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
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

        #region XNA methods

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

        public static void Flush()
        {
            lastGamepadState = gamepadState;
        }

        #endregion

        #region Gamepad region

        public static bool ButtonReleased(Buttons button)
        {
            return gamepadState.IsButtonUp(button) && lastGamepadState.IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button)
        {
            return gamepadState.IsButtonDown(button) && lastGamepadState.IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button)
        {
            return gamepadState.IsButtonDown(button);
        }

        #endregion
    }
}
