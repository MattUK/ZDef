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



        bool Colliding;

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
            Colliding = false;
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

                if (Colliding == false)
                {
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
            }
            else
            {
                NotMoving = true;
            }

            //if (Target != null)
            //{
            //    throw new Exception("");
            //}

            if (CurrentTile != null)
            {
                Building building = tileMap.GetBuildingAt(CurrentTile.TilePos().X, CurrentTile.TilePos().Y);

                //throw new Exception("w");

                if (ZDefGame.tileMap.GetTerrainTile(CurrentTile.TilePos().X, CurrentTile.TilePos().Y).GetTileType() == TileType.WATER)
                {
                    Colour.A = 50;
                }
                else
                {
                    Colour = Color.White;
                }

                if (building != null)
                {
                    if (building.GetTileType().tileID == 80)
                    {
                        Colliding = true;
                        building.Health -= 1;

                        if (building.Health == 0)
                        {
                            tileMap.ClearBuilding(CurrentTile.TilePos().X, (CurrentTile.TilePos().Y));
                        }

                    }
                }
                else
                {
                    Colliding = false;
                }
            }

            // if (Target == null)
            //  {
            GetTarget(HumanList);
            // }
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Disabled the lighting to test "underwater"bies.
            spriteBatch.Draw(Texture, Position, null, Colour, Rotation, Origin, Scale, SpriteEffects.None, ZDefGame.HUMAN_DRAW_DEPTH);
            // spriteBatch.Draw(Texture, Position, null,Colour ZDefGame.lightMap.GetLightColour((int)CurrentTile.TilePos().X, (int)CurrentTile.TilePos().Y), Rotation, Origin, Scale, SpriteEffects.None, ZDefGame.HUMAN_DRAW_DEPTH);
        }
    }
}
