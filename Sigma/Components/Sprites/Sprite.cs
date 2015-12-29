using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sigma.Components.Sprites
{
    /// <summary>
    /// This class represents a base sprite with no animation with a rather fiex location.
    /// This sprite might be interactuable, in which case it generates an interaction rectangle for the game to detect an intersection
    /// with the player's rectangle.
    /// </summary>
    public class Sprite
    {
        #region Field region
        protected Texture2D texture;
        protected Rectangle sourceRectangle;
        protected Vector2 position;
        protected bool isInteractuable;
        #endregion
        #region Properties region
        public int Width
        {
            get { return sourceRectangle.Width; }
        }

        public int Height
        {
            get { return sourceRectangle.Height; }
        }

        public Rectangle SpriteRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, Width, Height); }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsInteractuable
        {
            get { return isInteractuable; }
            set { isInteractuable = value; }
        }

        public Rectangle InteractionArea
        {
            get
            {
                if (isInteractuable)
                {
                    Rectangle interaction = new Rectangle((int)position.X, (int)position.Y, Width, Height);
                    interaction.Inflate(2, 2);
                    return interaction;
                } else
                {
                    return new Rectangle((int)position.X, (int)position.Y, 0, 0);
                }
            }
        }
        #endregion
        #region Constructor region
        public Sprite(Texture2D image, Rectangle sourceRectangle, bool interactuable)
        {
            texture = image;
            this.sourceRectangle = sourceRectangle;
            position = Vector2.Zero;
            isInteractuable = interactuable;
        }
        #endregion
        #region Monogame methods
        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
        }
        #endregion
    }
}
