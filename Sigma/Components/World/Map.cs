using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TiledSharp;
using System;

namespace Sigma.Components.World
{
    /// <summary>
    /// This class represents a single map. 
    /// Contains a TmxMap from TiledSharp library, a reference of the game for content loading purposes,
    /// a list of textures where the tilesets images come from and a list of rectangles representing
    /// all collision boxes to be analyzed in each Update loop of the GamePlayScreen class.
    /// </summary>
    public class Map
    {
        #region Fields region
        Game1 gameRef;
        TmxMap map;
        string mapId;
        List<Texture2D> textures;
        List<Rectangle> collisionRects, eventRects;
        #endregion

        #region Properties region
        public TmxMap MapFile
        {
            get { return map; }
            set { map = value; }
        }

        public string MapId
        {
            get { return mapId; }
        }

        public List<Texture2D> Textures
        {
            get { return textures; }
        }

        public List<Rectangle> CollisionBoxes
        {
            get { return collisionRects; }
        }

        public List<Rectangle> EventBoxes
        {
            get { return eventRects; }
        }

        public short  MapTileWidth
        {
            get { return (short)map.TileWidth; }
        }

        public short MapTileHeight
        {
            get { return (short)map.TileHeight; }
        }

        public int TilesWide
        {
            get { return map.Width; }
        }

        public int TilesHigh
        {
            get { return map.Height; }
        }

        public int PixelsWidth
        {
            get { return TilesWide * MapTileWidth; }
        }

        public int PixelsHeight
        {
            get { return TilesHigh * MapTileHeight; }
        }
        #endregion

        #region Constructor region
        /// <summary>
        /// Main map class constructor. Initialices the TmxMap field and lists, adding a reference of the current game.
        /// </summary>
        /// <param name="game">A reference of the current game for content loading purposes</param>
        /// <param name="mapFile">The name of the tmx file, which will be also the id or tag of the current map.</param>
        public Map (Game game, string mapFile)
        {
            gameRef = (Game1)game;
            map = new TmxMap("Content/Maps/MapFiles/" + mapFile + ".tmx");
            textures = new List<Texture2D>();
            collisionRects = new List<Rectangle>();
            eventRects = new List<Rectangle>();
            mapId = mapFile;
        }
        #endregion

        #region Monogame methods
        public void LoadContent()
        {
            for(int i = 0; i < map.Tilesets.Count; i++)
            {
                Texture2D tilesetLayer;
                tilesetLayer = gameRef.Content.Load<Texture2D>(@"Maps\Tilesets\" + map.Tilesets[i].Name);
                textures.Add(tilesetLayer);
            }
            
        }

        public void Initialize()
        {
            foreach(TmxObjectGroup tmxObject in map.ObjectGroups)
            {
                string objectGroupName = tmxObject.Name;
                foreach(TmxObject objects in tmxObject.Objects)
                {
                    Rectangle rect = new Rectangle((int)objects.X, (int)objects.Y, (int)objects.Width, (int)objects.Height);
                    if (objectGroupName.Equals("Collisions"))
                        CollisionBoxes.Add(rect);
                    else
                        EventBoxes.Add(rect);
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int tileGid = 0;
            Rectangle drawTileRect, tilesetRect;
            foreach(TmxLayer layer in map.Layers)
            {
                if(!layer.Name.Equals("AboveSprite"))
                {
                    for (int i = 0; i < layer.Tiles.Count; i++)
                    {
                        if (layer.Tiles[i].Gid != 0)
                        {
                            int tilesetId = SearchTileSet(layer.Tiles[i].Gid);
                            //System.Console.WriteLine("Para gid {0} devuelve tileset {1}", layer.Tiles[i].Gid, SearchTileSet(layer.Tiles[i].Gid));
                            TmxTileset tileset = map.Tilesets[tilesetId];
                            //System.Console.WriteLine("De modo que seleccionamos el tileset {0}", tileset.Name);
                            tileGid = layer.Tiles[i].Gid - tileset.FirstGid;
                            //System.Console.WriteLine("Tilegid = {0}", tileGid);
                            int tilesPerRow = (int)textures[tilesetId].Width / tileset.TileWidth;
                            //System.Console.WriteLine("Tiles por fila: {0}", tilesPerRow);
                            int tilesetCol = tileGid / tilesPerRow;
                            int tilesetRow = tileGid % tilesPerRow;
                            
                            //System.Console.WriteLine("Fila: {0}, Columna: {1}", tilesetRow, tilesetCol);
                            float coordinateX = (i % map.Width) * map.TileWidth;
                            float coordinateY = (i / map.Height) * map.TileHeight;
                            //System.Console.WriteLine("Se usa la textura {0}", textures[tilesetId].Name);
                            drawTileRect = new Rectangle(tilesetRow * tileset.TileWidth, tilesetCol * tileset.TileHeight, tileset.TileWidth, tileset.TileHeight);
                            tilesetRect = new Rectangle((int)coordinateX, (int)coordinateY, tileset.TileWidth, tileset.TileHeight);

                            spriteBatch.Draw(textures[tilesetId], tilesetRect, drawTileRect, Color.White);
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods region
        /// <summary>
        /// Private method that returns the tileset index of a given tile general id (gid)
        /// by comparing its value with each tileset's first gid the map uses.
        /// </summary>
        /// <param name="gid">Index of the tile</param>
        /// <returns>An integer being the index of the tileset, to be used with the field "textures" where tilesets Texture2D are being stored.</returns>
        private int SearchTileSet(int gid)
        {
            for(int i = map.Tilesets.Count - 1; i >= 0; i--)
            {
                int firstgid = map.Tilesets[i].FirstGid;
                if (gid > firstgid)
                    return i;
            }
            return -1;
        }
        #endregion
    }
}
