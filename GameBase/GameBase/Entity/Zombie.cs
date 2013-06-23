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
        Human Target;
        int ClosestDistance;
        int DistanceMax; //Search range is essentially entire map
        Tile CurrentTile;
        Tile GoalTile;
        List<Vector2> Path;
        bool NotMoving;
        Vector2 MoveGoal;

        public Zombie(Texture2D Tex, Tile ChosenTile)
        {
            Texture = Tex;
            Position = ChosenTile.GetPosition() + new Vector2(16, 16);
            ConstructThings();
            ClosestDistance = 10000;
            DistanceMax = ClosestDistance;
            CurrentTile = ChosenTile;
            Path = new List<Vector2>();
            NotMoving = true;
            MoveGoal = Position;
        }

        public void Update(List<Human> HumanList, Pathfinder pathFinder, SelectionHandle Select, TileMap tileMap)
        {
            CurrentTile = Select.GetSelectedTile(tileMap, Position);

            if (Target != null)
            {
                Path = pathFinder.FindPath(CurrentTile.TilePos(), Target.CurrentTile.TilePos());
               
                if (Path.Count == 0)
                {
                    Target = null;
                }
            }


            if (Path.Count != 0 && NotMoving == true)
            {
                MoveGoal = Path[0];
                Path.RemoveAt(0);
            }

            if (MoveGoal != Position)
            {
                NotMoving = false;

                if (Position.X > MoveGoal.X)
                {
                    Position.X -= 0.5f;
                }
                if (Position.Y > MoveGoal.Y)
                {
                    Position.Y -= 0.5f;
                }
                if (Position.X < MoveGoal.X)
                {
                    Position.X += 0.5f;
                }
                if (Position.Y < MoveGoal.Y)
                {
                    Position.Y += 0.5f;
                }
            }
            else
            {
                NotMoving = true;
            }

            //if (Target != null)
            //{
            //    throw new Exception("");
            //}

            if (Target == null)
            {
                GetTarget(HumanList);
            }
        }

        void GetTarget(List<Human> HumanList)
        {

            for (int i = 0; i < HumanList.Count; i++)
            {
                if (Vector2.Distance(Position, HumanList[i].Position) < ClosestDistance)
                {
                    Target = HumanList[i];
                    ClosestDistance = (int)Vector2.Distance(Position, HumanList[i].Position);
                }
            }

            ClosestDistance = DistanceMax;
        }
    }
}
