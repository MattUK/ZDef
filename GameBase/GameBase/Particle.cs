using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameBase
{
    class Particle
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle;
        public float AngleVelocity;
        public Color Colour;
        public float Scale;
        public int TimeToLive;

        public Vector2 Origin;
        public bool Cloud;

        public Sprite Parent;

        public Particle(Texture2D Tex, Vector2 Pos, Vector2 Vec, float Ang, float AngVec, Color Col, float Size, int Life, bool cloud, Sprite Par)
        {
            Texture = Tex;
            Position = Pos;
            Velocity = Vec;
            Angle = Ang;
            AngleVelocity = AngVec;
            Colour = Col;
            Scale = Size;
            TimeToLive = Life;
            Origin = new Vector2(Tex.Width / 2, Tex.Height / 2);
            Cloud = cloud;
            Parent = Par;
        }

        public void Update()
        {
            TimeToLive--;

            if (Cloud == true)
            {
                Position += Velocity; //Spurty Trail
            }
            else
            {
                Position += Velocity / 5; //Line Trail
            }

            Angle += AngleVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Colour, Angle, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
