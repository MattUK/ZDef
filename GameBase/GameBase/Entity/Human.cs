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
        public int Health; //Help me....
        public Tile CurrentTile; //Tile that holds this human.
        public Tile GoalTile;
        Vector2 MoveGoal;
        List<Vector2> Path;

        public Human(Texture2D Tex, Tile ChosenTile)
        {
            Position = ChosenTile.GetPosition();
            Texture = Tex;
            Scale = 1.0f;
            CurrentTile = ChosenTile;
            GoalTile = CurrentTile;
        }

        public void Update(Pathfinder pathFinder, InputHandler Input)
        {
            if (CurrentTile != GoalTile)
            {
                Path = pathFinder.FindPath(CurrentTile.TilePos(), GoalTile.TilePos());
                CurrentTile = GoalTile;
            }

            if (Path.Count != 0)
            {
                MoveGoal = Path[0];
                Path.RemoveAt(0);
            }
        }

        public void BuildThing()
        {

        }

        public void SetGoal(Tile tile)
        {
            GoalTile = tile;
        }

    }
}
