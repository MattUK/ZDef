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
    class Sprite
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Color Colour;
        public Vector2 Origin;
        public float Rotation;
        public bool Dead;
        public float Scale;

        public Color[] ColArray;
        public Matrix TransformMatrix;
        public Rectangle BoundingRect;

        public int SpriteWidth;
        public int SpriteHeight;

        public void ConstructThings()
        {
            Vector2 vec = new Vector2(Texture.Width / 2, Texture.Height / 2);

            SpriteWidth = Texture.Width;
            SpriteHeight = Texture.Height;

            ColArray = new Color[SpriteWidth * SpriteHeight];
            Texture.GetData(ColArray);

            Origin = vec;
        }

        public void UpdateBoundingBox()
        {
            TransformMatrix = Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateTranslation(new Vector3(Position, 0.0f));
            BoundingRect = CalculateBoundingRectangle(new Rectangle(0, 0, SpriteWidth, SpriteHeight), TransformMatrix);
        }

        Rectangle CalculateBoundingRectangle(Rectangle rectangle, Matrix transform)
        {
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);


            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));


            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        public void Move(float Speed)
        {
            Vector2 Direction = new Vector2(((float)Math.Cos(Rotation)), ((float)Math.Sin(Rotation)));
            Position -= Direction * -Speed;
        }

        public Rectangle GetBox()
        {
            Rectangle rect = new Rectangle((int)Position.X, (int)Position.Y, SpriteWidth, SpriteHeight);
            return rect;
        }

        public void Remove()
        {
            Dead = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 1.0f);
        }
    }
}
