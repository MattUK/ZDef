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
    public class Zombie : Sprite
    {
        Sprite Target;

        public Zombie(Texture2D Tex, Tile ChosenTile, float Rot)
        {
            Texture = Tex;
            Position = ChosenTile.GetPosition() + new Vector2(16, 16);
            Rotation = Rot;
            ConstructThings();
        }

        public void Update(Sprite Target)
        {
            if (Target != null)
            {
                float XDis = (Position.X - Target.Position.X);
                float YDis = (Position.Y - Target.Position.Y);

                Rotation = (float)Math.Atan2(-YDis, -XDis);
            }

            Move(0.9f);
        }

        void GetTarget(Sprite target)
        {

        }
    }
}
