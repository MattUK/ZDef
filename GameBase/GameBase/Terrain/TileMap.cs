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

        }

    }
}
