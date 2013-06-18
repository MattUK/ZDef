using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameBase.Terrain
{
    public class TileType
    {

        public static TileType GRASS = new TileType(0, 64, 64, new Vector2(0.0f, 0.0f));
        public static TileType STONE = new TileType(1, 64, 64, new Vector2(64.0f, 0.0f));
        public static TileType WATER = new TileType(2, 64, 64, new Vector2(128.0f, 0.0f), false);

        public int tileID;
        public int tileWidth, tileHeight;
        public Vector2 positionInSheet;
        public bool passable;

        public TileType(int id, int tileWidth, int tileHeight, Vector2 positionInSheet, bool passable = true)
        {
            this.tileID = id;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.positionInSheet = positionInSheet;
        }

        public Rectangle GetSourcePositionRectangle()
        {
            return new Rectangle((int)positionInSheet.X, (int)positionInSheet.Y, tileWidth, tileHeight);
        }

    }
}
