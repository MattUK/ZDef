using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;
using Microsoft.Xna.Framework;
using GameBase.Entity;

namespace GameBase
{
    public class TileMap
    {
        private Tile[,] largeTiles;
        private Tile[,] smallTiles;

        private int mapWidth, mapHeight;
        private Heightmap heightmap;

        public TileMap(int width, int height)
        {
            this.mapWidth = width;
            this.mapHeight = height;
            this.largeTiles = new Tile[width, height];
            this.smallTiles = new Tile[width * 2, height * 2];

            for (int i = 0; i < smallTiles.GetLength(0); i++)
            {
                for (int j = 0; j < smallTiles.GetLength(1); j++)
                {
                    smallTiles[i, j] = new Tile(i, j, TileType.EMPTY_SMALL);
                }
            }

            heightmap = new Heightmap(width, height);

            CreateInitialTerrain();
        }

        public int GetWidth()
        {
            return mapWidth;
        }

        public int GetHeight()
        {
            return mapHeight;
        }

        public Tile GetEntityTile(int x, int y)
        {
            return smallTiles[x, y];
        }

        public Tile GetTerrainTile(int x, int y)
        {
            return largeTiles[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)];
        }

        public bool IsPassable(int x, int y)
        {
            return GetEntityTile(x, y).GetTileType().passable && GetTerrainTile(x, y).GetTileType().passable;
        }

        public Tile[,] GetLargeTileMap()
        {
            return largeTiles;
        }

        public Tile[,] GetSmallTileMap()
        {
            return smallTiles;
        }

        public void CreateInitialTerrain()
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    largeTiles[i, j] = new Tile(i, j, heightmap.GetTypeAt(i, j), 1.0f);
                }
            }

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    if (largeTiles[i, j].GetTileType() == TileType.WATER)
                    {
                        //if (i > 0 && tiles[i - 1, j].GetTileType() != TileType.WATER)
                        //{
                        //    tiles[i, j].drawLeftBorder = true;
                        //}
                        if (i < mapWidth - 1 && largeTiles[i + 1, j].GetTileType() != TileType.WATER)
                        {
                            largeTiles[i, j].drawRightBorder = true;
                        }
                        if (j > 0 && largeTiles[i, j - 1].GetTileType() != TileType.WATER)
                        {
                            largeTiles[i, j].drawTopBorder = true;
                        }
                        //if (j < mapHeight - 1 && tiles[i, j + 1].GetTileType() != TileType.WATER)
                        //{
                        //    tiles[i, j].drawBottomBorder = true;
                        //}
                    }
                }
            }

        }

        public void Update()
        {
            float ambientLight = ZDefGame.lighting.GetLightLevel();

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    largeTiles[i, j].SetLightLevel(ambientLight);
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    largeTiles[i, j].Draw();
                }
            }

            for (int i = 0; i < mapWidth * 2; i++)
            {
                for (int j = 0; j < mapHeight * 2; j++)
                {
                    smallTiles[i, j].Draw();
                }
            }
        }

    }
}
