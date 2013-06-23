using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameBase.Entity;

namespace GameBase.Terrain
{
    public class Tile
    {
        private int i, j; // Position of this tile in the map array
        private TileType tileType; // Tile type

        public bool drawTopBorder, drawBottomBorder, drawLeftBorder, drawRightBorder;

        public Tile(int i, int j, TileType type)
        {
            this.i = i;
            this.j = j;
            this.tileType = type;

            drawTopBorder = drawBottomBorder = drawLeftBorder = drawRightBorder = false;
        }

        /// <summary>
        /// Returns the position of this tile on the map.
        /// </summary>
        /// <returns>Vector2 containing the position.</returns>
        public Vector2 GetPosition()
        {
            return new Vector2(i * tileType.tileWidth, j * tileType.tileHeight);
        }

        /// <summary>
        /// Returns the tile type this tile is assigned.
        /// </summary>
        /// <returns>The TileType.</returns>
        public TileType GetTileType()
        {
            return tileType;
        }

        public void SetTileType(TileType type)
        {
            tileType = type;
        }

        public Point TilePos()
        {
            return new Point(i, j);
        }

        /// <summary>
        /// Returns the rectangle of this tile.
        /// </summary>
        /// <returns>The bounding rectangle that encompasses this tile.</returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle(i * tileType.tileWidth, j * tileType.tileHeight, tileType.tileWidth, tileType.tileHeight);
        }

        public bool Passable()
        {
            return tileType.passable;
        }

        /// <summary>
        /// Override in child classes.
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Draws this tile at its position on the world map.
        /// </summary>
        public void Draw(int smallTileX, int smallTileY)
        {
            Color drawColour = ZDefGame.lightMap.GetLightColour(smallTileX, smallTileY);

            if (tileType.tileID < 50)
            {
                ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetRectangle(), tileType.GetSourcePositionRectangle(), drawColour, 0.0f, new Vector2(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, ZDefGame.TERRAIN_DRAW_DEPTH);

                if (drawTopBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetRectangle(), TileType.BORDER_TOP.GetSourcePositionRectangle(), drawColour, 0.0f, new Vector2(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, ZDefGame.TERRAIN_DRAW_DEPTH - 0.1f);
                }
                if (drawBottomBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetRectangle(), TileType.BORDER_BOTTOM.GetSourcePositionRectangle(), drawColour, 0.0f, new Vector2(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, ZDefGame.TERRAIN_DRAW_DEPTH - 0.1f);
                }
                if (drawLeftBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetRectangle(), TileType.BORDER_LEFT.GetSourcePositionRectangle(), drawColour, 0.0f, new Vector2(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, ZDefGame.TERRAIN_DRAW_DEPTH - 0.1f);
                }
                if (drawRightBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetRectangle(), TileType.BORDER_RIGHT.GetSourcePositionRectangle(), drawColour, 0.0f, new Vector2(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, ZDefGame.TERRAIN_DRAW_DEPTH - 0.1f);
                }
            }
            else
            {
                ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, GetRectangle(), tileType.GetSourcePositionRectangle(), drawColour, 0.0f, new Vector2(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, ZDefGame.BUILDING_DRAW_DEPTH);
            }
        }

    }
}
