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

namespace GameBase.Entity
{
    public class Human : Sprite
    {
        public bool Selected;
        public int Health;

        public Human(Vector2 Pos, Texture2D Tex)
        {
            Position = Pos;
            Texture = Tex;
            Selected = false;
            Scale = 1.0f;
        }

        public void BuildThing()
        {

        }

    }
}
