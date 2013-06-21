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
using GameBase.Terrain;

namespace GameBase.Entity
{
    class Zombie : Sprite
    {
        public Zombie(Texture2D Tex, Vector2 Pos, float Rot)
        {
            Texture = Tex;
            Position = Pos;
            Rotation = Rot;
            ConstructThings();
        }

        public void Update()
        {
        }
    }
}
