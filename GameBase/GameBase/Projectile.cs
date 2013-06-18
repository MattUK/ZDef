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
    class Projectile : Sprite
    {
        public int Speed;
        public int Damage;
        public bool Active;

        public Projectile(Vector2 Pos, Texture2D Tex, float Rot, int Sped, int Dmg)
        {
            Position = Pos;
            Texture = Tex;
            Rotation = Rot;
            Speed = Sped;
            Dead = false;
            Damage = Dmg;

            Origin = GetOrigin();
        }

        public void Update()
        {
            Move(Speed);
        }
    }
}
