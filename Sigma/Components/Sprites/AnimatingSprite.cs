using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sigma.Components.Sprites
{
    public class AnimatingSprite : Sprite
    {
        Dictionary<AnimationType, Animation> frames;
        AnimationType currentAnimationType;
        bool isAnimating = false;

        public AnimationType CurrentAnimationType
        {
            get { return currentAnimationType; }
            set { currentAnimationType = value; }
        }

        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public AnimatingSprite(Texture2D texture, Dictionary<AnimationType, Animation> animations)
            :base(texture, new Rectangle(0, 0, texture.Width, texture.Height), true)
        {
            frames = new Dictionary<AnimationType, Animation>();
            foreach(AnimationType type in animations.Keys)
            {
                frames.Add(type, (Animation)animations[type]);
            }
        }

        public AnimatingSprite(Texture2D texture, Dictionary<AnimationType, Animation> animations, Rectangle rect)
            :base(texture, rect, true)
        {
            frames = new Dictionary<AnimationType, Animation>();
            foreach (AnimationType type in animations.Keys)
            {
                frames.Add(type, (Animation)animations[type]);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (isAnimating)
            {
                frames[currentAnimationType].Update(gameTime);
            }
            //base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, frames[currentAnimationType].CurrentRectangleFrame, Color.White);
        }
    }
}
