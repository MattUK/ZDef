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

        public Vector2 GetOrigin()
        {
            Vector2 vec = new Vector2(Texture.Width / 2, Texture.Height / 2);
            return vec;
        }

        public void Move(int Speed)
        {
            Vector2 Direction = new Vector2(((float)Math.Cos(Rotation)), ((float)Math.Sin(Rotation)));
            Position -= Direction * Speed;
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
