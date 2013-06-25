#define SEIZURE_MODE

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
        public const bool DRAW_BORDER = false;

        private Tile[,] largeTiles;
        private Tile[,] smallTiles;

        private Building[,] smallBuildings;
        private Building[,] largeBuildings;

        private int mapWidth, mapHeight;
        private Heightmap heightmap;
        public bool WallChanged;

        public Vector2 borderLarge;
        public bool drawLargeBorder;

        public Vector2 borderSmall;
        public bool drawSmallBorder;

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

            // ==== Handle Buildings ====
            smallBuildings = new Building[width * 2, height * 2];
            largeBuildings = new Building[width, height];

            WallChanged = false;

            CreateInitialTerrain();
        }

        public Building GetBuildingAt(int x, int y)
        {
            if (!IsBuildingAt(x, y)) {
                return null;
            }

            if (largeBuildings[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)] != null)
            {
                return largeBuildings[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)];
            }
            else if (smallBuildings[x, y] != null)
            {
                return smallBuildings[x, y];
            }
            return null;
        }

        public bool IsBuildingAt(int x, int y)
        {
            if (x < 0 || y < 0 || x > (mapWidth * 2) - 1 || y > (mapHeight * 2) - 1)
            {
                return false;
            }
            if (!IsPassable(x, y)) {
                return true;
            }
            return ((largeBuildings[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)] != null) || (smallBuildings[x, y] != null));
        }

        public bool CreateSmallBuildingAt(int x, int y, Building building)
        {
            if (x < 0 || y < 0 || x > (mapWidth * 2) - 1 || y > (mapHeight * 2) - 1)
            {
                return false;
            }
            smallBuildings[x, y] = building;
            smallTiles[x, y] = new Tile(x, y, building.GetTileType());
            return true;
        }

        public bool CreateLargeBuildingAt(int smallXPosition, int smallYPosition, Building building)
        {
            if (smallXPosition < 0 || smallYPosition < 0 || smallXPosition > (mapWidth * 2) - 1 || smallYPosition > (mapHeight * 2) - 1)
            {
                return false;
            }
            largeBuildings[(int)Math.Floor(smallXPosition / 2.0f), (int)Math.Floor(smallYPosition / 2.0f)] = building;
            largeTiles[(int)Math.Floor(smallXPosition / 2.0f), (int)Math.Floor(smallYPosition / 2.0f)] = new Tile((int)Math.Floor(smallXPosition / 2.0f), (int)Math.Floor(smallYPosition / 2.0f), building.GetTileType());
            return true;
        }

        public void ClearBuilding(int x, int y)
        {
            if (IsBuildingAt(x, y))
            {
                if (smallBuildings[x, y] != null)
                {
                    smallBuildings[x, y] = null;
                    smallTiles[x, y] = new Tile(x, y, TileType.EMPTY_SMALL);
                    ZDefGame.pathFinder.EnvironmentChanged = true;
                    ZDefGame.zombiePathfinder.EnvironmentChanged = true;
                }
                if (largeBuildings[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)] != null)
                {
                    largeBuildings[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)] = null;
                    largeTiles[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)] = new Tile(x, y, TileType.EMPTY_LARGE);
                    ZDefGame.pathFinder.EnvironmentChanged = true;
                    ZDefGame.pathFinder.EnvironmentChanged = true;
                }
            }
        }

        public int GetWidth()
        {
            return mapWidth;
        }

        public int GetHeight()
        {
            return mapHeight;
        }

        public bool SetSmallTile(int x, int y, Tile tile)
        {
            if (x < 0 || y < 0 || x > (mapWidth * 2) - 1 || y > (mapHeight * 2) - 1)
            {
                return false;
            }
            else
            {
                smallTiles[x, y] = tile;
                return true;
            }
        }

        public bool SetLargeTile(int x, int y, Tile tile)
        {
            if (x < 0 || y < 0 || x > (mapWidth * 2) - 1 || y > (mapHeight * 2) - 1)
            {
                return false;
            }
            else
            {
                largeTiles[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)] = tile;
                return true;
            }
        }

        public Tile GetEntityTile(int x, int y)
        {
            if (x < 0 || y < 0 || x > (mapWidth * 2) - 1 || y > (mapHeight * 2) - 1)
            {
                return null;
            }
            return smallTiles[x, y];
        }

        public Tile GetTerrainTile(int x, int y)
        {
            if (x < 0 || y < 0 || x > (mapWidth * 2) - 1 || y > (mapHeight * 2) - 1)
            {
                return null;
            }
            return largeTiles[(int)Math.Floor(x / 2.0f), (int)Math.Floor(y / 2.0f)];
        }

        public bool IsPassable(int x, int y)
        {
            if (x < 0 || y < 0 || x > (mapWidth * 2) - 1 || y > (mapHeight * 2) - 1)
            {
                return false;
            }
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
                    largeTiles[i, j] = new Tile(i, j, heightmap.GetTypeAt(i, j));
                }
            }

            PerlinNoise2D treeNoiseGen = new PerlinNoise2D(mapWidth * mapHeight * DateTime.Now.Millisecond);
            treeNoiseGen.setOctaveCount(1);
            treeNoiseGen.setFrequency(0.1f);

            for (int i = 0; i < mapWidth * 2; i++)
            {
                for (int j = 0; j < mapHeight * 2; j++)
                {
                    float value = MathHelper.Clamp(treeNoiseGen.generateNoiseValue(i, j), 0.0f, 1.0f);
                    if (value > 0.9f)
                    {
                        //Why not just check if the tile is a grass tile?
                        if (GetTerrainTile(i, j).GetTileType() != TileType.WATER && GetTerrainTile(i, j).GetTileType() != TileType.SAND && GetTerrainTile(i, j).GetTileType() != TileType.STONE)
                        {
                            Tree tree = new Tree();
                            tree.OnUserInteract(new Building.EntityInteraction(Building.EntityInteraction.Interaction.DESTROYING, null, 0));
                            CreateSmallBuildingAt(i, j, tree);
                        }
                    }
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
            for (int i = 0; i < mapWidth * 2; i++)
            {
                for (int j = 0; j < mapHeight * 2; j++)
                {
                    if (largeBuildings[(int)Math.Floor(i / 2.0f), (int)Math.Floor(j / 2.0f)] != null)
                    {
                        largeBuildings[(int)Math.Floor(i / 2.0f), (int)Math.Floor(j / 2.0f)].Update(this, (int)Math.Floor(i / 2.0f), (int)Math.Floor(j / 2.0f));

                        if (largeBuildings[(int)Math.Floor(i / 2.0f), (int)Math.Floor(j / 2.0f)].Dead)
                        {
                            ClearBuilding(i, j);
                        }
                    }

                    if (smallBuildings[i, j] != null)
                    {
                        if (WallChanged == false)
                        {
                            if (smallBuildings[i, j].GetTileType().tileID != 80)
                            {
                                smallBuildings[i, j].Update(this, i, j);
                            }
                        }
                        else
                        {
                            smallBuildings[i, j].Update(this, i, j);
                        }
                        if (smallBuildings[i, j].Dead)
                        {
                            ClearBuilding(i, j);
                        }
                    }
                }
            }
            WallChanged = false;
        }

        public void Draw()
        {
            for (int i = 0; i < mapWidth * 2; i++)
            {
                for (int j = 0; j < mapHeight * 2; j++)
                {
                    // Large tiles
                    int transformedLargeX = (int)Math.Floor(i / 2.0f);
                    int transformedLargeY = (int)Math.Floor(j / 2.0f);

                    if (largeTiles[transformedLargeX, transformedLargeY].GetTileType().tileID > 50)
                    {
                        if (largeBuildings[transformedLargeX, transformedLargeY] != null)
                        {
                            largeBuildings[transformedLargeX, transformedLargeY].Draw(largeTiles[transformedLargeX, transformedLargeY], i, j);
                        }
                    }
                    else
                    {
                        largeTiles[transformedLargeX, transformedLargeY].Draw(i, j);
                    }

                    if (ZDefGame.GameGUI.currentBuilding != null)
                    {
                        if (ZDefGame.GameGUI.placing && ZDefGame.GameGUI.currentBuilding.Large && ZDefGame.Selection.SelectedTile != null && ZDefGame.Selection.SelectedTile.TilePos().X == i && ZDefGame.Selection.SelectedTile.TilePos().Y == j)
                        {
                            ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, new Rectangle(transformedLargeX * 64, transformedLargeY * 64, 64, 64), new Rectangle(96, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
                        }
                    }

                    // Small tiles
                    if (smallTiles[i, j].GetTileType().tileID > 50)
                    {
                        if (smallBuildings[i, j] != null)
                        {
                            smallBuildings[i, j].Draw(smallTiles[i, j], i, j);
                        }
                    }
                    else
                    {
                        smallTiles[i, j].Draw(i, j);
                    }

                    if (ZDefGame.GameGUI.currentBuilding != null)
                    {
                        if (ZDefGame.GameGUI.placing && !ZDefGame.GameGUI.currentBuilding.Large && ZDefGame.Selection.SelectedTile != null && ZDefGame.Selection.SelectedTile.TilePos().X == i && ZDefGame.Selection.SelectedTile.TilePos().Y == j)
                        {
                            ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, new Rectangle(i * 32, j * 32, 32, 32), new Rectangle(288, 0, 32, 32), Color.White, 0.0f, Vector2.Zero, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
                        }
                    }
                }
            }

            if (ZDefGame.GameGUI.currentBuilding == null && ZDefGame.Selection.SelectedTile != null && ZDefGame.GameGUI.deleting)
            {
                int SX = ZDefGame.Selection.SelectedTile.TilePos().X;
                int SY = ZDefGame.Selection.SelectedTile.TilePos().Y;

                Building b = GetBuildingAt(SX, SY);
                if (b != null)
                {
                    if (b.Large)
                    {
                        Tile tile = largeTiles[(int)Math.Floor(SX / 2.0f), (int)Math.Floor(SY / 2.0f)];
                        ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, new Rectangle((int)tile.GetPosition().X, (int)tile.GetPosition().Y, 64, 64), new Rectangle(96, 0, 64, 64), Color.White, 0.0f, Vector2.Zero, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
                    }
                    else
                    {
                        Tile tile = smallTiles[SX, SY];
                        ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, new Rectangle((int)tile.GetPosition().X, (int)tile.GetPosition().Y, 32, 32), new Rectangle(288, 0, 32, 32), Color.White, 0.0f, Vector2.Zero, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
                    }
                }
            }

            if (DRAW_BORDER == true)
            {
                for (int i = 0; i < mapWidth * 2; i++)
                {
                    for (int j = 0; j < mapHeight * 2; j++)
                    {
                        ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, new Vector2(i * 32, j * 32), TileType.SMALL_BORDER.GetSourcePositionRectangle(), Color.White);
                    }
                }
            }

            //foreach (Vector2 light in lights)
            //{
            //    ZDefGame.spriteBatch.Draw(ZDefGame.lightTexture, new Vector2((light.X * 32) - (128 / 2), (light.Y * 32) - (128 / 2)), Color.White);
            //}
        }

    }
}
