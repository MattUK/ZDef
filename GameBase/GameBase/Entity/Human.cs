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
    public class Human : Sprite
    {
        public bool Selected; //Is this human chosen by god?
        public int Health; //Help me....
        public Tile CurrentTile; //Tile that holds this human.

        public Human(Texture2D Tex, Tile ChosenTile)
        {
            Position = ChosenTile.GetPosition();
            Texture = Tex;
            Selected = false;
            Scale = 1.0f;
            CurrentTile = ChosenTile;
        }



        public void BuildThing()
        {

        }

    }
}
