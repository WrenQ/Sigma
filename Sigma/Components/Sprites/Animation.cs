using System.Collections.Generic;
using System.Linq;


using Microsoft.Xna.Framework;

namespace Sigma.Components.Sprites
{
    /// <summary>
    /// Enumeration representing all possible animations
    /// </summary>
    public enum AnimationType
    {
        WalkingUp,
        WalkingDown,
        WalkingLeft,
        WalkingRight
    }

    /// <summary>
    /// This class manages the visible portion of a spritesheet when animating,
    /// changing current frame each `frameDuration' game frames (usually 30).
    /// </summary>
    public class Animation
    {
        #region Fields region
        List<Rectangle> frames;
        int currentFrame, frameDuration, frameWidth, frameHeight;
        int timer = 0;
        #endregion
        #region Properties region
        public Rectangle CurrentRectangleFrame
        {
            get { return frames.ElementAt<Rectangle>(currentFrame); }
        }

        public int FrameDuration
        {
            get { return frameDuration; }
            set { frameDuration = value; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = MathHelper.Clamp(value, 0, frames.Count - 1);
            }
        }

        public int FrameWidth
        {
            get { return frameWidth; }
        }

        public int FrameHeight
        {
            get { return frameHeight; }
        }

        public int Timer
        {
            get { return timer; }
            set
            {
                timer = MathHelper.Clamp(value, 0, frameDuration);
            }
        }
        #endregion
        #region Constructor region
        public Animation(int numberFrames, int frameWidth, int frameHeight, int rowShift, int columnShift, int duration)
        {
            frames = new List<Rectangle>(numberFrames);
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            for(int i = 0; i < numberFrames; i++)
            {
                frames[i] = new Rectangle(
                    rowShift + i * frameWidth,
                    columnShift,
                    frameWidth,
                    frameHeight);
            }

            currentFrame = 0;
            frameDuration = duration;
        }
        #endregion
        #region Monogame region
        public void Update(GameTime gameTime)
        {
            Timer++;
            if(Timer == FrameDuration)
            {
                currentFrame = (currentFrame + 1) & frames.Count;
                Timer = 0;
            }
        }
        #endregion
    }
}
