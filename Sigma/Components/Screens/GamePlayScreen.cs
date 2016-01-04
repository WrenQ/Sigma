using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TiledSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Sigma.Components.Input;
using Sigma.Components.Sprites;
using Sigma.Components.GameStates;
using Sigma.Components.World;

namespace Sigma.Components.Screens
{
    public class GamePlayScreen : BaseGameScreen
    {
        #region Fields region
        Map map;
        //short currentMapIndex = 0;
        AnimatingSprite player;
        #endregion

        #region Properties region
        /*public TmxMap CurrentMap
        {
            get { return maps[currentMapIndex]; }
        }*/

        public Map GameMap
        {
            get { return map; }
        }

        public AnimatingSprite Player
        {
            get { return player; }
        }

        #endregion

        #region Constructor region
        public GamePlayScreen(Game game, ScreenManager manager)
            :base(game, manager)
        { }
        #endregion

        #region Monogame Methods
        protected override void LoadContent()
        {
            base.LoadContent();
            map = new Map(this.gameRef, "Meru");
            map.LoadContent();
            Texture2D indraSprite = gameRef.Content.Load<Texture2D>(@"Graphics/Spritesheets/SpriteIndra");
            Dictionary<AnimationType, Animation> animations = new Dictionary<AnimationType, Animation>();
            Animation animDOWN = new Animation(4, 32, 48, 0, 0, 30);
            Animation animLEFT = new Animation(4, 32, 48, 0, 48, 30);
            Animation animRIGHT = new Animation(4, 32, 48, 0, 96, 30);
            Animation animUP = new Animation(4, 32, 48, 0, 144, 30);
            animations.Add(AnimationType.WalkingDown, animDOWN);
            animations.Add(AnimationType.WalkingLeft, animLEFT);
            animations.Add(AnimationType.WalkingRight, animRIGHT);
            animations.Add(AnimationType.WalkingUp, animUP);
            player = new AnimatingSprite(indraSprite, animations);
            player.Position = new Vector2(0, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            gameRef.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            base.Draw(gameTime);
            map.Draw(gameRef.SpriteBatch);
            player.Draw(gameTime, gameRef.SpriteBatch);
                       
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);

        }
        #endregion
    }
}
