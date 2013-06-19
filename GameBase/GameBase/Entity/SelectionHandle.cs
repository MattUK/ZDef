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

        public SelectionHandle(InputHandler Input)
        {
            this.Input = Input;
            SelectedHuman = null;
        }

        public void Update(List<Human> HumanList, Camera2D Camera, TileMap tileMap)
        {
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
                    SelectedHuman.SetGoal(SelectedTile);
                }
            }
        }

        void CheckSelected(Human human, Vector2 MousePos, List<Human> HumanList)
        {
            if (human.Position.X < MousePos.X)
            {
                if (human.Position.Y < MousePos.Y)
                {
                    if (human.Position.X + human.Texture.Width > MousePos.X)
                    {
                        if (human.Position.Y + human.Texture.Height > MousePos.Y)
                        {
                            SelectedHuman = human;
                        }
                    }
                }
            }
        }

        Tile GetSelectedTile(TileMap tileMap, Vector2 MousePos)
        {
            float tileX = (MousePos.X) / 32;
            float tileY = (MousePos.Y) / 32;
          
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
