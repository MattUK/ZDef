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
        private float lightLevel;// Light level

        public bool drawTopBorder, drawBottomBorder, drawLeftBorder, drawRightBorder;

        public Tile(int i, int j, TileType type, float lightLevel = 1.0f)
        {
            this.i = i;
            this.j = j;
            this.tileType = type;
            SetLightLevel(lightLevel);

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

        /// <summary>
        /// Returns the light level of this tile, from 0 to 1.
        /// </summary>
        /// <returns>The light level.</returns>
        public float GetLightLevel()
        {
            return lightLevel;
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

        /// <summary>
        /// Sets the light level (from 0 to 1).
        /// </summary>
        /// <param name="lightLevel">The new light level of this tile.</param>
        public void SetLightLevel(float lightLevel)
        {
            lightLevel = MathHelper.Clamp(lightLevel, 0.0f, 1.0f);
            this.lightLevel = lightLevel;
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
        public void Draw()
        {
            Color drawColour = Color.White * lightLevel;
            drawColour.A = 255;

            if (tileType.tileID < 50)
            {
                ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetPosition(), tileType.GetSourcePositionRectangle(), drawColour);

                if (drawTopBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetPosition(), TileType.BORDER_TOP.GetSourcePositionRectangle(), drawColour);
                }
                if (drawBottomBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetPosition(), TileType.BORDER_BOTTOM.GetSourcePositionRectangle(), drawColour);
                }
                if (drawLeftBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetPosition(), TileType.BORDER_LEFT.GetSourcePositionRectangle(), drawColour);
                }
                if (drawRightBorder)
                {
                    ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, GetPosition(), TileType.BORDER_RIGHT.GetSourcePositionRectangle(), drawColour);
                }
            }
            else
            {
                ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, GetPosition(), tileType.GetSourcePositionRectangle(), drawColour);
            }
        }

    }
}
