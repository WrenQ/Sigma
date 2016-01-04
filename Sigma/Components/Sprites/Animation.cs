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
        short currentFrame, frameDuration, frameWidth, frameHeight;
        short timer = 0;
        bool isLoop = true;
        #endregion
        #region Properties region
        public Rectangle CurrentRectangleFrame
        {
            get { return frames.ElementAt<Rectangle>(currentFrame); }
        }

        public short FrameDuration
        {
            get { return frameDuration; }
            set { frameDuration = value; }
        }

        public short CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = (short)MathHelper.Clamp(value, 0, frames.Count - 1);
            }
        }

        public short FrameWidth
        {
            get { return frameWidth; }
        }

        public short FrameHeight
        {
            get { return frameHeight; }
        }

        public short Timer
        {
            get { return timer; }
            set
            {
                timer = (short)MathHelper.Clamp(value, 0, frameDuration);
            }
        }

        public bool IsLoop
        {
            get { return isLoop; }
            set { isLoop = value; }
        }
        #endregion
        #region Constructor region
        public Animation(short numberFrames, short frameWidth, short frameHeight, short rowShift, short columnShift, short duration)
        {
            frames = new List<Rectangle>();
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            for(int i = 0; i < numberFrames; i++)
            {
                Rectangle rect = new Rectangle(
                    rowShift + (i * frameWidth),
                    columnShift,
                    frameWidth,
                    frameHeight);
                frames.Add(rect);
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
                if (isLoop)
                {
                    currentFrame = (short)((currentFrame + 1) % frames.Count);
                }
                else
                {
                    if(currentFrame < frames.Count - 1)
                    {
                        currentFrame++;
                    }
                }
                Timer = 0;
            }
        }
        #endregion
        #region Methods region
        public void ResetAnimation()
        {
            Timer = 0;
            currentFrame = 0;
        }

        public void SwitchAnimation()
        {
            if (isLoop)
                isLoop = false;
            else
                isLoop = true;
        }
        #endregion
    }
}
