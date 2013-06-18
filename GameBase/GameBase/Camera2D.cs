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
    class Camera2D
    {
        public Vector2 Position;
        public float Zoom;
        public float Rotation;
        int Height;
        int Width;

        public Camera2D(Vector2 Pos, int W, int H)
        {
            Position = Pos;  //Starting position of the Camera.
            Zoom = 1.0f;     //Zoom level of the Camera;
            Rotation = 0.0f; //Rotation value of the camera.
            Width = W;       //Camera view width  (screen width).
            Height = H;      //Camera view height (screen height).
        }

        public void Input(InputHandler InputHandle)
        {
            //Keyboard input to move the camera around.

            if (InputHandle.KeyDown(Keys.W) == true)
            {
                Position.Y -= 10;
            }
            if (InputHandle.KeyDown(Keys.S) == true)
            {
                Position.Y += 10;
            }
            if (InputHandle.KeyDown(Keys.A) == true)
            {
                Position.X -= 10;
            }
            if (InputHandle.KeyDown(Keys.D) == true)
            {
                Position.X += 10;
            }


            if (InputHandle.KeyDown(Keys.Q) == true)
            {
                Zoom += 0.03f;
            }
            if (InputHandle.KeyDown(Keys.E) == true)
            {
                Zoom -= 0.03f;
            }

            if (InputHandle.ScrollIn() == true)
            {
                Zoom += 0.15f;
            }

            if (InputHandle.ScrollOut() == true)
            {
                Zoom -= 0.15f;
            }
        }

        public void Constraint(Vector2 Max)
        {
            //Constrains the camera to the provided values, ensures it cannot move off screen.

            if (Position.X - Width / 2 < 0)
            {
                Position.X = Width / 2;
            }
            if (Position.Y - Height / 2 < 0)
            {
                Position.Y = Height / 2;
            }
            if (Position.X + Width / 2 > Max.X)
            {
                Position.X = Max.X - Width / 2;
            }
            if (Position.Y + Height / 2 > Max.Y)
            {
                Position.Y = Max.Y - Height / 2;
            }

            if (Zoom > 4f)
            {
                Zoom = 4f;
            }
            if (Zoom < 0.2500001f)
            {
                Zoom = 0.2500001f;
            }
        }

        public Matrix Transform()
        {
            Matrix Transformation =
            Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
            //Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
            Matrix.CreateTranslation(new Vector3(Width / 2, Height / 2, 0));

            return Transformation;
        }
    }
}
