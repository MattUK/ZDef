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

namespace GameBase
{
    public class InputHandler
    {
        KeyboardState key;
        KeyboardState keyLast;
        MouseState Mousey;
        MouseState MouseyLast;
        int CurrentScrollValue;
        int LastScrollValue;

        /// <summary>
        /// Updates values, call this at the start of your game loop.
        /// </summary>
        public void UpdateFirstValues()
        {
            key = Keyboard.GetState();
            Mousey = Mouse.GetState();
            CurrentScrollValue = Mousey.ScrollWheelValue;
        }

        /// <summary>
        /// Updates values, call this at the end of your game loop.
        /// </summary>
        public void UpdateLastValues()
        {
            LastScrollValue = Mousey.ScrollWheelValue;
            keyLast = Keyboard.GetState();
            MouseyLast = Mousey;
        }

        /// <summary>
        /// Returns the position of the mouse as a vector 2.
        /// </summary>
        /// <returns></returns>
        public Vector2 MousePosition()
        {
            Vector2 Position = new Vector2(Mousey.X, Mousey.Y);
            return Position;
        }

        /// <summary>
        /// Gets the actual position of the mouse in the game world.
        /// </summary>
        public Vector2 TanslatedMousePos(Camera2D Camera)
        {
            Vector2 Position = Vector2.Transform(MousePosition(), Matrix.Invert(Camera.Transform()));
            return Position;
        }

        /// <summary>
        /// Returns true if the left mouse button is clicked.
        /// </summary>
        /// <returns></returns>
        public bool LeftClick()
        {
            if (Mousey.LeftButton == ButtonState.Released)
            {
                if (MouseyLast.LeftButton == ButtonState.Pressed)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the left mouse button is clicked.
        /// </summary>
        /// <returns></returns>
        public bool RightClick()
        {
            if (Mousey.RightButton == ButtonState.Released)
            {
                if (MouseyLast.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the left mouse button is held down.
        /// </summary>
        /// <returns></returns>
        public bool LeftDown()
        {
            if (Mousey.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the relevant key has been pressed.
        /// </summary>
        /// <param name="Keys KeyPress"></param>
        /// <returns></returns>
        public bool KeyClicked(Keys KeyPress)
        {
            if (keyLast.IsKeyDown(KeyPress))
            {
                if (key.IsKeyUp(KeyPress))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the relevant key is being held.
        /// </summary>
        /// <param name="Keys KeyPress"></param>
        /// <returns></returns>
        public bool KeyDown(Keys KeyPress)
        {
            if (key.IsKeyDown(KeyPress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the relevant key is not in use.
        /// </summary>
        /// <param name="Keys KeyPress"></param>
        /// <returns></returns>
        public bool KeyUp(Keys KeyPress)
        {
            if (key.IsKeyUp(KeyPress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the scroll wheel has been moved down.
        /// </summary>
        /// <returns></returns>
        public bool ScrollOut()
        {
            if (CurrentScrollValue < LastScrollValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the scroll wheel has been moved up.
        /// </summary>
        /// <returns></returns>
        public bool ScrollIn()
        {
            if (CurrentScrollValue > LastScrollValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
