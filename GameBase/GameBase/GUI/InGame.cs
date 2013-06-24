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
        bool Pressed;

        public Button(Vector2 Pos, int width, int height, Texture2D Tex)
        {
            Pressed = false;
            Texture = Tex;
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
    }
}
