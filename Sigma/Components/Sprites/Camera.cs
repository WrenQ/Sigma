using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Sigma.Components.Input;

namespace Sigma.Components.Sprites
{
    public enum CameraMode
    {
        Movable,
        PlayerCentered
    }

    public class Camera
    {
        Vector2 position;
        Rectangle viewportRectangle;
        CameraMode mode;
        float speed;
        Game1 gameRef;

        public Vector2 CameraPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle ViewportRectangle
        {
            get { return viewportRectangle; }
            set { viewportRectangle = value; }
        }

        public CameraMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        
        public Game1 GameRef
        {
            get { return gameRef; }
        }

        public Camera(Rectangle viewportRectangle, Game game)
        {
            speed = 3f;
            this.viewportRectangle = viewportRectangle;
            mode = CameraMode.PlayerCentered;
            gameRef = (Game1)game;
        }

        public Camera(Rectangle viewportRectangle, Vector2 position, Game game)
        {
            speed = 3f;
            this.viewportRectangle = viewportRectangle;
            this.position = position;
            mode = CameraMode.PlayerCentered;
            gameRef = (Game1)game;
        }

        public void Update(GameTime gameTime)
        {
            if (mode != CameraMode.PlayerCentered)
            {
                Vector2 motion = Vector2.Zero;
                if (InputHandler.ButtonDown(Buttons.RightThumbstickLeft)
                    || InputHandler.ButtonDown(Buttons.DPadLeft))
                    motion.X = -speed;
                else if (InputHandler.ButtonDown(Buttons.RightThumbstickRight)
                    || InputHandler.ButtonDown(Buttons.DPadRight))
                    motion.X = speed;
                if (InputHandler.ButtonDown(Buttons.RightThumbstickUp)
                    || InputHandler.ButtonDown(Buttons.DPadUp))
                    motion.Y = -speed;
                else if (InputHandler.ButtonDown(Buttons.RightThumbstickDown)
                    || InputHandler.ButtonDown(Buttons.DPadDown))
                    motion.Y = speed;

                if(motion != Vector2.Zero)
                {
                    motion.Normalize();
                    position = position + (motion * speed);
                    position.X = MathHelper.Clamp(position.X, 0, GameRef.GamePlayScreen.GameMap.PixelsWidth- viewportRectangle.Width);
                    position.X = MathHelper.Clamp(position.X, 0, GameRef.GamePlayScreen.GameMap.PixelsHeight - viewportRectangle.Height);
                }
            }
        }
    }
}
