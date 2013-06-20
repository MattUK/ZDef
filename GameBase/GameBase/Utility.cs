using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;


namespace GameBase
{
    public class Utility
    {
        public int TotalFrames;
        public float ElaspedTime;
        public int FPS;

        public void Update(GameTime gameTime)
        {
            //FPS Counter
            ElaspedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElaspedTime >= 1000.0f)
            {

                FPS = TotalFrames;
                TotalFrames = 0;
                ElaspedTime = 0;
            }

            Console.WriteLine(FPS);
            //FPS Counter
        }

        public void UpdateFrames()
        {
            TotalFrames++;
        }

        public void TrapMouse(bool Trap, ZDefGame Game)
        {
            if (Trap == false)
            {
                Cursor.Clip = new System.Drawing.Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            }
            else
            {
                Cursor.Clip = new System.Drawing.Rectangle(Game.Window.ClientBounds.X, Game.Window.ClientBounds.Y, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
            }
        }


    }
}
