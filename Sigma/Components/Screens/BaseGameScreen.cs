using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Sigma.Components.GameStates;

namespace Sigma.Components.Screens
{
    public class BaseGameScreen : GameScreen
    {
        protected Game1 gameRef;
        Texture2D blankTexture;
        protected Rectangle fadingScreen;
        protected Song backgroundMusic;
        
        public Song BGM
        {
            get { return backgroundMusic; }
            set { backgroundMusic = value; }
        }

        public BaseGameScreen(Game game, ScreenManager manager)
            :base(game, manager)
        {
            gameRef = (Game1)game;
        }

        protected override void LoadContent()
        {
            blankTexture = Game.Content.Load<Texture2D>(@"Graphics\blank");
            fadingScreen = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            gameRef.SpriteBatch.Draw(blankTexture, fadingScreen, fadingScreen, Color.Black * Alpha);
        }
    }
}
