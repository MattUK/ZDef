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
        public static TileType STONE = new TileType(1, 64, 64, new Vector2(64.0f, 0.0f), false);
        public static TileType WATER = new TileType(2, 64, 64, new Vector2(128.0f, 0.0f), false);
        public static TileType SAND = new TileType(3, 64, 64, new Vector2(192.0f, 0.0f));

        // =========== Large Tiles =============
        public static TileType EMPTY_LARGE = new TileType(50, 64, 64, new Vector2(448.0f, 448.0f));
        public static TileType WOODEN_WATCH_TOWER = new TileType(51, 64, 64, new Vector2(160.0f, 0.0f), false);
        public static TileType BLUE_TENT = new TileType(52, 64, 64, new Vector2(0.0f, 0.0f), false);
        public static TileType DROP_PAD = new TileType(53, 64, 64, new Vector2(224.0f, 0.0f), false);

        // =========== Small Tiles =============
        public static TileType EMPTY_SMALL = new TileType(60, 32, 32, new Vector2(448.0f, 480.0f));
        public static TileType SMALL_BORDER = new TileType(60, 32, 32, new Vector2(288.0f, 0.0f));

        public static TileType WOODEN_BARRICADE = new TileType(61, 32, 32, new Vector2(224.0f, 64.0f), false);

        // Wooden wall stuff
        public static TileType WALL_VERTICAL = new TileType(80, 32, 32, new Vector2(0.0f, 64.0f), false);
        public static TileType WALL_HORIZONTAL = new TileType(81, 32, 32, new Vector2(32.0f, 64.0f), false);
        public static TileType WALL_TOP_TO_LEFT = new TileType(82, 32, 32, new Vector2(0.0f, 96.0f), false);
        public static TileType WALL_TOP_TO_RIGHT = new TileType(83, 32, 32, new Vector2(32.0f, 96.0f), false);
        public static TileType WALL_RIGHT_TO_BOTTOM = new TileType(84, 32, 32, new Vector2(64.0f, 96.0f), false);
        public static TileType WALL_LEFT_TO_BOTTOM = new TileType(85, 32, 32, new Vector2(96.0f, 96.0f), false);
        public static TileType WALL_VERTICAL_WITH_LEFT = new TileType(86, 32, 32, new Vector2(0.0f, 128.0f), false);
        public static TileType WALL_VERTICAL_WITH_RIGHT = new TileType(87, 32, 32, new Vector2(32.0f, 128.0f), false);
        public static TileType WALL_HORIZONTAL_WITH_BOTTOM = new TileType(88, 32, 32, new Vector2(64.0f, 128.0f), false);
        public static TileType WALL_HORIZONTAL_WITH_TOP = new TileType(89, 32, 32, new Vector2(96.0f, 128.0f), false);
        public static TileType WALL_ALL_FOUR = new TileType(90, 32, 32, new Vector2(0.0f, 160.0f), false);

        public static TileType BORDER_LEFT = new TileType(-1, 64, 64, new Vector2(192, 128));
        public static TileType BORDER_RIGHT = new TileType(-2, 64, 64, new Vector2(0, 128));
        public static TileType BORDER_TOP = new TileType(-3, 64, 64, new Vector2(128, 128));
        public static TileType BORDER_BOTTOM = new TileType(-4, 64, 64, new Vector2(64, 128));

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
            this.passable = passable;
        }

        public Rectangle GetSourcePositionRectangle()
        {
            return new Rectangle((int)positionInSheet.X, (int)positionInSheet.Y, tileWidth, tileHeight);
        }

    }
}
