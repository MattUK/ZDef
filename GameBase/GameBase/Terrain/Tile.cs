using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameBase.Terrain
{
    class Tile
    {
        public const int DEFAULT_TILE_WIDTH = 64; // Default tile width
        public const int DEFAULT_TILE_HEIGHT = 64; // Default tile height

        private int i, j; // Position of this tile in the map array
        private TileType tileType; // Tile tyep
        private float lightLevel;// Light level

        public Tile(int i, int j, TileType type, float lightLevel = 1.0f)
        {
            this.i = i;
            this.j = j;
            this.tileType = type;
            setLightLevel(lightLevel);
        }

        /// <summary>
        /// Returns the position of this tile on the map.
        /// </summary>
        /// <returns>Vector2 containing the position.</returns>
        public Vector2 getPosition()
        {
            return new Vector2(i * DEFAULT_TILE_WIDTH, j * DEFAULT_TILE_HEIGHT);
        }

        /// <summary>
        /// Returns the tile type this tile is assigned.
        /// </summary>
        /// <returns>The TileType.</returns>
        public TileType getTileType()
        {
            return tileType;
        }

        /// <summary>
        /// Returns the light level of this tile, from 0 to 1.
        /// </summary>
        /// <returns>The light level.</returns>
        public float getLightLevel()
        {
            return lightLevel;
        }

        /// <summary>
        /// Returns the rectangle of this tile.
        /// </summary>
        /// <returns>The bounding rectangle that encompasses this tile.</returns>
        public Rectangle getRectangle()
        {
            return new Rectangle(i * DEFAULT_TILE_WIDTH, j * DEFAULT_TILE_HEIGHT, DEFAULT_TILE_WIDTH, DEFAULT_TILE_HEIGHT);
        }

        /// <summary>
        /// Sets the light level (from 0 to 1).
        /// </summary>
        /// <param name="lightLevel">The new light level of this tile.</param>
        public void setLightLevel(float lightLevel)
        {
            lightLevel = MathHelper.Clamp(lightLevel, 0.0f, 1.0f);
            this.lightLevel = lightLevel;
        }

        /// <summary>
        /// Override in child classes.
        /// </summary>
        public void update()
        {

        }

        /// <summary>
        /// Draws this tile at its position on the world map.
        /// </summary>
        public void draw()
        {
            ZDefGame.spriteBatch.Draw(ZDefGame.terrainTexture, getPosition(), tileType.getSourcePositionRectangle(), Color.White * lightLevel);
        }

    }
}
