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

        public int Health = 125;

        List<Vector2> shadowList;
        bool hidden;

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

            shadowList = new List<Vector2>();
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
                    hidden = true;
                }
                else
                {
                    Colour = ZDefGame.lightMap.GetLightColour(CurrentTile.TilePos().X, CurrentTile.TilePos().Y);
                    hidden = false;
                }

                if (building != null)
                {
                    if (building.GetName() == "WALL")
                    {
                        building.OnUserInteract(new GameBase.Entity.Building.EntityInteraction(Building.EntityInteraction.Interaction.DESTROYING, this, 1));
                        Colliding = true;
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
            shadowList.Clear();

            Vector2 zombiePos = new Vector2((int)Math.Floor(CurrentTile.TilePos().X / 2.0f), (int)Math.Floor(CurrentTile.TilePos().Y / 2.0f));

            if (!hidden)
            {
                for (int i = 0; i < ZDefGame.lightMap.GetLightCount(); i++)
                {
                    Vector2 lightPos = ZDefGame.lightMap.GetLight(i);

                    float currentDistance = Vector2.Distance(lightPos, zombiePos);

                    if (currentDistance <= 7.0f && currentDistance > 1.0f)
                    {
                        shadowList.Add(lightPos);
                    }
                    else if (currentDistance <= 1.0f)
                    {
                        shadowList.Clear();
                        break;
                    }
                }


                foreach (Vector2 v in shadowList)
                {
                    float amountPerLightLevel = 1 / (LightMap.MAXIMUM_LIGHT_LEVEL - LightMap.MINIMUM_LIGHT_LEVEL);
                    float currentAmount = 1 - (amountPerLightLevel * (ZDefGame.lightMap.GetLightLevel() - 0.3f));

                    float shadRotation = (float)Math.Atan2(zombiePos.Y - v.Y, zombiePos.X - v.X);
                    Color shadowColour = new Color(1.0f, 1.0f, 1.0f, currentAmount);

                    spriteBatch.Draw(ZDefGame.shadowTexture, Position, null, shadowColour, shadRotation, new Vector2(0.0f, 16.0f), 1.0f, SpriteEffects.None, ZDefGame.HUMAN_DRAW_DEPTH + 0.1f);
                }
            }

            //Disabled the lighting to test "underwater"bies.
            spriteBatch.Draw(Texture, Position, null, Colour, Rotation, Origin, Scale, SpriteEffects.None, ZDefGame.HUMAN_DRAW_DEPTH);
        }
    }
}
