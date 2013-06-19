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
        private Tile[,] terrainTiles;
        private Tile[,] buildingTiles;

        private int mapWidth, mapHeight;
        private Heightmap heightmap;

        public TileMap(int width, int height)
        {
            this.mapWidth = width;
            this.mapHeight = height;
            this.terrainTiles = new Tile[width, height];
            this.buildingTiles = new Tile[width * 2, height * 2];

            for (int i = 0; i < buildingTiles.GetLength(0); i++)
            {
                for (int j = 0; j < buildingTiles.GetLength(1); j++)
                {
                    buildingTiles[i, j] = new Tile(i, j, TileType.EMPTY_BUILDING);
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
            return buildingTiles[x, y];
        }

        public Tile GetTerrainTile(int x, int y)
        {
            return terrainTiles[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)];
        }

        public bool IsPassable(int x, int y)
        {
            return GetEntityTile(x, y).GetTileType().passable && GetTerrainTile(x, y).GetTileType().passable;
        }

        public void CreateInitialTerrain()
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    terrainTiles[i, j] = new Tile(i, j, heightmap.GetTypeAt(i, j), 1.0f);
                }
            }

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    if (terrainTiles[i, j].GetTileType() == TileType.WATER)
                    {
                        //if (i > 0 && tiles[i - 1, j].GetTileType() != TileType.WATER)
                        //{
                        //    tiles[i, j].drawLeftBorder = true;
                        //}
                        if (i < mapWidth - 1 && terrainTiles[i + 1, j].GetTileType() != TileType.WATER)
                        {
                            terrainTiles[i, j].drawRightBorder = true;
                        }
                        if (j > 0 && terrainTiles[i, j - 1].GetTileType() != TileType.WATER)
                        {
                            terrainTiles[i, j].drawTopBorder = true;
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
                    terrainTiles[i, j].SetLightLevel(ambientLight);
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    terrainTiles[i, j].Draw();
                }
            }

            for (int i = 0; i < mapWidth * 2; i++)
            {
                for (int j = 0; j < mapHeight * 2; j++)
                {
                    buildingTiles[i, j].Draw();
                }
            }
        }

    }
}
