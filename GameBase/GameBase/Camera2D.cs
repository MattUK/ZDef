﻿using System;
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
    public class Camera2D
    {
        public Vector2 Position;
        public float Zoom;
        public float Rotation;
        int Height;
        int Width;
        bool MaxZoom;
        bool MinZoom;

        public Camera2D(Vector2 Pos, int W, int H)
        {
            Position = Pos;  //Starting position of the Camera.
            Zoom = 1.0f;     //Zoom level of the Camera;
            Rotation = 0.0f; //Rotation value of the camera.
            Width = W;       //Camera view width  (screen width).
            Height = H;      //Camera view height (screen height).

            MaxZoom = false;
            MinZoom = false;
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

            if (InputHandle.MousePosition().X > -5 && InputHandle.MousePosition().X < 15)
            {
                Position.X -= 10;
            }
            if (InputHandle.MousePosition().Y > -5 && InputHandle.MousePosition().Y < 15)
            {
                Position.Y -= 10;
            }
            if (InputHandle.MousePosition().Y < Height + 5 && InputHandle.MousePosition().Y > Height - 15)
            {
                Position.Y += 10;
            }
            if (InputHandle.MousePosition().X < Width + 5 && InputHandle.MousePosition().X > Width - 15)
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

            if (InputHandle.ScrollIn() == true && MinZoom == false)
            {
                Zoom += 0.1f;
                MaxZoom = false;
            }

            if (InputHandle.ScrollOut() == true && MaxZoom == false)
            {
                Zoom -= 0.1f;
                MinZoom = false;
            }
        }

        public void Constraint(Vector2 Max)
        {
            //Constrains the camera to the provided values, ensures it cannot move off screen.

            if (Zoom > 1f)
            {
                Zoom = 1f;
                MinZoom = true;
            }
            if (Zoom < 0.3500001f)
            {
                Zoom = 0.3500001f;
                MaxZoom = true;
            }


            Vector2 CamMin = Vector2.Transform(Vector2.Zero, Matrix.Invert(Transform()));
            Vector2 CamSize = new Vector2(Width, Height) / Zoom;
            Vector2 limitMin = new Vector2(0, 0);
            Vector2 limitMax = new Vector2(Max.X, Max.Y);
            Vector2 PosOffset = Position - CamMin;
            Position = Vector2.Clamp(CamMin, limitMin, limitMax - CamSize) + PosOffset;

            //if (Position.X - Vector2.Transform(new Vector2((Width / 2), 0), ZoomTransform()).X < 0)
            //{
            //    Position.X = Vector2.Transform(new Vector2((Width / 2), 0), ZoomTransform()).X;
            //}
            //if (Position.Y - (Height / 2) < 0)
            //{
            //    Position.Y = (Height / 2);
            //}
            //if (Position.X + (Width / 2) > Max.X)
            //{
            //    Position.X = Max.X - (Width / 2);
            //}
            //if (Position.Y + (Height / 2) > Max.Y)
            //{
            //    Position.Y = Max.Y - (Height / 2);
            //
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

        Matrix ZoomTransform()
        {
            Matrix transformation = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            return transformation;
        }
    }
}
