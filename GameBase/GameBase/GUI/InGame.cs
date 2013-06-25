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
        public bool toggled;

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
                if (Contains(Input.MousePosition()))
                {
                    Pressed = true;
                }
            }
        }

        public void ToggleTexture(bool Active)
        {
            if (Active == true)
            {
                Texture = ActiveTexture;
                toggled = true;
            }
            else
            {
                Texture = InactiveTexture;
                toggled = false;
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
        public bool deleting;

        public InGame(ContentManager Content)
        {
            placing = false;
            ButtonList = new List<Button>();
            Button WButton = new Button(new Vector2(0, 550), 50, 50, Content.Load<Texture2D>("GuiBoxWALL"), Content.Load<Texture2D>("GuiBoxWALLInactive"), new Wall(100, 100));
            Button WTButton = new Button(new Vector2(50, 550), 50, 50, Content.Load<Texture2D>("GuiBoxWATCHTOWER"), Content.Load<Texture2D>("GuiBoxWATCHTOWERInactive"), new WatchTower(100, 100));
            Button DButton = new Button(new Vector2(100, 550), 50, 50, Content.Load<Texture2D>("GuiBoxDELETE"), Content.Load<Texture2D>("GuiBoxDELETEInactive"), null);
            ButtonList.Add(WButton);
            ButtonList.Add(WTButton);
            ButtonList.Add(DButton);
        }

        public void Update(InputHandler Input)
        {
            if (!placing)
            {
                currentBuilding = null;

                for (int i = 0; i < ButtonList.Count; i++)
                {
                    bool active = false;
                    ButtonList[i].Update(Input);

                    if (ButtonList[i].Pressed)
                    {
                        active = true;
                        placing = true;

                        if (ButtonList[i].associatedBuilding == null)
                        {
                            deleting = true;
                        }
                    }

                    if (active || (ButtonList[i].associatedBuilding == currentBuilding && deleting))
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
            }
            else
            {
                for (int i = 0; i < ButtonList.Count; i++)
                {
                    ButtonList[i].Update(Input);
                    if (ButtonList[i].Pressed && ButtonList[i].toggled)
                    {
                        placing = false;
                        currentBuilding = null;

                        if (deleting)
                        {
                            deleting = false;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ButtonList.Count; i++)
            {
                ButtonList[i].Draw(spriteBatch);
            }

            spriteBatch.Draw(ZDefGame.guiStatus, new Vector2(0.0f, 0.0f), Color.White);
            spriteBatch.DrawString(ZDefGame.spriteFont, "" + ZDefGame.player.GetResourceCount(), new Vector2(13, 4), Color.White);
            spriteBatch.DrawString(ZDefGame.spriteFont, "" + ZDefGame.player.inGameTime.ToShortTimeString(), new Vector2(65, 4), Color.White);
        }
    }
}
