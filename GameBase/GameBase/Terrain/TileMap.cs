using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase
{
    class TileMap
    {

        private Tile[,] tiles;
        private int mapWidth, mapHeight;

        public TileMap(int width, int height)
        {
            this.mapWidth = width;
            this.mapHeight = height;
            this.tiles = new Tile[width, height];
        }

        public void createInitialTerrain()
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    tiles[i, j] = new Tile(i, j, TileType.GRASS, 1.0f);
                }
            }
        }

        public void update()
        {

        }

        public void draw()
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    tiles[i, j].draw();
                }
            }
        }

    }
}
