using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBase.Terrain;

namespace GameBase.Entity
{
    public class SelectionHandle
    {
        InputHandler Input;
        Human SelectedHuman;
        Tile SelectedTile;
        bool TileValid;

        public SelectionHandle(InputHandler Input)
        {
            this.Input = Input;
            SelectedHuman = null;
        }

        public void Update(List<Human> HumanList, Camera2D Camera, TileMap tileMap, Pathfinder pathFinder)
        {
            TileValid = true;
            SelectedTile = GetSelectedTile(tileMap, Input.TanslatedMousePos(Camera));

            if (SelectedTile != null)
            {
                if (Input.KeyClicked(Keys.Z))
                {
                    ZDefGame.SpawnRifleZombie(SelectedTile.TilePos().X, SelectedTile.TilePos().Y);
                }
                if (Input.KeyClicked(Keys.X))
                {
                    ZDefGame.SpawnEngieZombie(SelectedTile.TilePos().X, SelectedTile.TilePos().Y);
                }
                if (Input.KeyClicked(Keys.C))
                {
                    ZDefGame.SpawnRifleman(SelectedTile.TilePos().X, SelectedTile.TilePos().Y);
                }
                if (Input.KeyClicked(Keys.V))
                {
                    ZDefGame.SpawnEngineer(SelectedTile.TilePos().X, SelectedTile.TilePos().Y);
                }
            }

            if (Input.KeyClicked(Keys.U))
            {
                ZDefGame.lightMap.AddPointLight((int)SelectedTile.TilePos().X, (int)SelectedTile.TilePos().Y, 1.0f);
            }

            if (Input.LeftClick() == true)
            {
                ClearSelected();

                for (int i = 0; i < HumanList.Count; i++)
                {
                    CheckSelected(HumanList[i], Input.TanslatedMousePos(Camera), HumanList);
                }
            }

            if (SelectedHuman != null)
            {
                if (Input.RightClick() == true)
                {
                    for (int i = 0; i < HumanList.Count; i++)
                    {
                        if (SelectedTile == HumanList[i].CurrentTile)
                        {
                            TileValid = false;
                        }
                    }

                    if (TileValid == true && SelectedTile != null)
                    {
                        SelectedHuman.SetGoal(SelectedTile);
                    }
                }
            }

            WallPlacement(Input, pathFinder);
        }

        void WallPlacement(InputHandler Input, Pathfinder pathFinder)
        {
            if (ZDefGame.GameGUI.PlacingWalls == true)
            {
                if (SelectedTile != null)
                {
                    if (Input.LeftDown() == true)
                    {
                        Wall wall = new Wall(100, 100);
                        wall.SpawnAt(ZDefGame.tileMap, SelectedTile.TilePos().X, SelectedTile.TilePos().Y);
                        pathFinder.EnvironmentChanged = true;
                        ZDefGame.tileMap.WallChanged = true;
                    }

                    //if (Input.KeyDown(Keys.D2))
                    //{
                    //    ZDefGame.tileMap.ClearBuilding(SelectedTile.TilePos().X, SelectedTile.TilePos().Y);
                    //}
                }

                if (Input.RightClick() == true)
                {
                    ZDefGame.GameGUI.PlacingWalls = false;
                }
            }
        }

        void CheckSelected(Human human, Vector2 MousePos, List<Human> HumanList)
        {
            if (human.Position.X < MousePos.X+16)
            {
                if (human.Position.Y < MousePos.Y+16)
                {
                    if (human.Position.X + human.Texture.Width > MousePos.X+16)
                    {
                        if (human.Position.Y + human.Texture.Height > MousePos.Y+16)
                        {
                            SelectedHuman = human;
                        }
                    }
                }
            }
        }

        public Tile GetSelectedTile(TileMap tileMap, Vector2 Pos)
        {
            float tileX = (Pos.X) / 32;
            float tileY = (Pos.Y) / 32;
          
            int X = (int)Math.Ceiling(tileX - 1);
            int Y = (int)Math.Ceiling(tileY - 1);

            if (X >= 0 && Y >= 0)
            {
                if (X < (tileMap.GetWidth() * 2) && Y < (tileMap.GetHeight() * 2))
                {
                    return tileMap.GetEntityTile(X, Y);
                }
            }

            //If no tile is returned, return null.
            return null;
        }

        void ClearSelected()
        {
            SelectedHuman = null;
        }
    }
}
