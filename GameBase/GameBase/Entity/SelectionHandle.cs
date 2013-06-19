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

namespace GameBase.Entity
{
    public class SelectionHandle
    {
        InputHandler Input;
        Human SelectedHuman;

        public SelectionHandle(InputHandler Input)
        {
            this.Input = Input;
            SelectedHuman = null;
        }

        public void Update(List<Human> HumanList, Camera2D Camera)
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
                    SelectedHuman.SetGoal 
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

        void ClearSelected()
        {
            SelectedHuman = null;
        }
    }
}
