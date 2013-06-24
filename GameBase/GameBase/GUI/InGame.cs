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
using GameBase.Entity;

namespace GameBase.GUI
{
    public class Button
    {
        Rectangle Box;
        Texture2D Texture;
        Texture2D ActiveTexture;
        Texture2D InactiveTexture;
        public bool Pressed;
        public Building associatedBuilding;

        public Button(Vector2 Pos, int width, int height, Texture2D IA, Texture2D A, Building associatedBuilding)
        {
            Pressed = false;
            Texture = IA;
            ActiveTexture = A;
            InactiveTexture = IA;
            Box = new Rectangle((int)Pos.X, (int)Pos.Y, width, height);
            this.associatedBuilding = associatedBuilding;
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
        public Building currentBuilding;
        public bool placing;
        List<Button> ButtonList;

        public InGame(ContentManager Content)
        {
            placing = false;
            ButtonList = new List<Button>();
            Button WButton = new Button(new Vector2(0, 550), 50, 50, Content.Load<Texture2D>("GuiBoxWALL"), Content.Load<Texture2D>("GuiBoxWALLInactive"), new Wall(100, 100));
            Button WTButton = new Button(new Vector2(50, 550), 50, 50, Content.Load<Texture2D>("GuiBoxWATCHTOWER"), Content.Load<Texture2D>("GuiBoxWATCHTOWERInactive"), new WatchTower(100, 100));
            ButtonList.Add(WButton);
            ButtonList.Add(WTButton);
        }

        public void Update(InputHandler Input)
        {
            for (int i = 0; i < ButtonList.Count; i++)
            {
                bool active = false;
                ButtonList[i].Update(Input);

                if (ButtonList[i].Pressed)
                {
                    active = true;
                    placing = true;
                }

                if (active || ButtonList[i].associatedBuilding == currentBuilding)
                {
                    ButtonList[i].ToggleTexture(true);
                    currentBuilding = ButtonList[i].associatedBuilding;
                    break;
                }
                else
                {
                    ButtonList[i].ToggleTexture(false);
                }
            }

            if (!placing)
            {
                currentBuilding = null;
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
