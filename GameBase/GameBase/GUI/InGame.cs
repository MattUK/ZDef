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

namespace GameBase.GUI
{
    public class Button
    {
        Rectangle Box;
        Texture2D Texture;
        Texture2D ActiveTexture;
        Texture2D InactiveTexture;
        public bool Pressed;

        public Button(Vector2 Pos, int width, int height, Texture2D IA, Texture2D A)
        {
            Pressed = false;
            Texture = IA;
            ActiveTexture = A;
            InactiveTexture = IA;
            Box = new Rectangle((int)Pos.X, (int)Pos.Y, width, height);
        }

        public void Update(InputHandler Input)
        {
            Pressed = false;

            if (Input.LeftClick() == true)
            {
                if(Contains(Input.MousePosition()))
                {
                    Pressed = true;
                }
            }

            //if (Pressed == true)
            //{
            //    Texture = ActiveTexture;
            //}
            //else
            //{
            //    Texture = InactiveTexture;
            //}
        }

        public void ToggleTexture(bool Active)
        {
            if (Active == true)
            {
                Texture = ActiveTexture;
            }
            else
            {
                Texture = InactiveTexture;
            }
        }

        bool Contains(Vector2 Pos)
        {
            if (Pos.X > Box.X && Pos.Y > Box.Y)
            {
                if (Pos.X < Box.X + Box.Width && Pos.Y < Box.Y + Box.Height)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Box, Color.White);
        }
    }

    public class InGame
    {
        public bool PlacingWalls;
        List<Button> ButtonList;

        public InGame(Texture2D WallButtonAct, Texture2D WallButtonInAct)
        {
            PlacingWalls = false;
            ButtonList = new List<Button>();
            Button WButton = new Button(new Vector2(0, 550), 50, 50, WallButtonAct, WallButtonInAct);
            ButtonList.Add(WButton);
        }

        public void Update(InputHandler Input)
        {
            for (int i = 0; i < ButtonList.Count; i++)
            {
                ButtonList[i].Update(Input);
            }

            if (ButtonList[0].Pressed == true)
            {
                PlacingWalls = true;
            }

            if (PlacingWalls == true)
            {
                ButtonList[0].ToggleTexture(true);
            }
            else
            {
                ButtonList[0].ToggleTexture(false);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ButtonList.Count; i++)
            {
                ButtonList[i].Draw(spriteBatch);
            }
        }
    }
}
