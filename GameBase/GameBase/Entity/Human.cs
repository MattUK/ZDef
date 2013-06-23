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
        bool NotMoving;
        List<Vector2> Path;

        public Weapon weapon;
        int ClosestDistance;
        int DistanceMax; //Range the rifleman can fire.

        List<Vector2> shadowList;

        public Human(Texture2D Tex, Tile ChosenTile, int WepDis, Texture2D BulTex)
        {
            Position = ChosenTile.GetPosition() + new Vector2(16, 16);
            MoveGoal = Position;
            Texture = Tex;
            CurrentTile = ChosenTile;
            GoalTile = CurrentTile;
            Path = new List<Vector2>();
            DistanceMax = WepDis;
            ClosestDistance = DistanceMax;
            ConstructThings();

            shadowList = new List<Vector2>();

            weapon = new Weapon(Position, Rotation, 20, 15, BulTex);
        }

        public void Update(Pathfinder pathFinder, InputHandler Input, SelectionHandle Select, TileMap tileMap, List<Zombie> ZombieList)
        {
            CurrentTile = Select.GetSelectedTile(tileMap, Position);

            FindTarget(ZombieList);

            if (Input.KeyClicked(Keys.R))
            {
                weapon.Fire();
                //throw new Exception("`12");
            }

            weapon.Update();
            weapon.Position = Position;

            if (CurrentTile != GoalTile)
            {
                Path = pathFinder.FindPath(CurrentTile.TilePos(), GoalTile.TilePos());
                //CurrentTile = GoalTile;
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
                    Position.X -= 2;
                }
                if (Position.Y > MoveGoal.Y)
                {
                    Position.Y -= 2;
                }
                if (Position.X < MoveGoal.X)
                {
                    Position.X += 2;
                }
                if (Position.Y < MoveGoal.Y)
                {
                    Position.Y += 2;
                }
            }
            else
            {
                NotMoving = true;
            }

            if (weapon.Target != null)
            {
                if (Vector2.Distance(Position, weapon.Target.Position) > DistanceMax)
                {
                    weapon.Target = null;
                }
            }

            weapon.Target = null;
        }


        public void BuildThing()
        {

        }

        /// <summary>
        /// ZombieList is a placeholder right now.
        /// </summary>
        void FindTarget(List<Zombie> ZombieList)
        {
            for (int i = 0; i < ZombieList.Count; i++)
            {
                if (Vector2.Distance(Position, ZombieList[i].Position) <= ClosestDistance)
                {
                    weapon.Target = ZombieList[i];
                    ClosestDistance = (int)Vector2.Distance(Position, ZombieList[i].Position);
                }
            }

            ClosestDistance = DistanceMax;
        }

        public void SetGoal(Tile tile)
        {
            GoalTile = tile;
            Path.Clear();
            NotMoving = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            shadowList.Clear();

            Vector2 humanPos = new Vector2((int)Math.Floor(CurrentTile.TilePos().X / 2.0f), (int)Math.Floor(CurrentTile.TilePos().Y / 2.0f));

            for (int i = 0; i < ZDefGame.lightMap.GetLightCount(); i++)
            {
                Vector2 lightPos = ZDefGame.lightMap.GetLight(i);

                float currentDistance = Vector2.Distance(lightPos, humanPos);

                if (currentDistance <= 4.0f && currentDistance > 1.0f)
                {
                    shadowList.Add(lightPos);
                }
                else if (currentDistance <= 1.0f)
                {
                    shadowList.Clear();
                    break;
                }
            }

            int shadowCount = 0;

            foreach (Vector2 v in shadowList)
            {
                shadowCount++;
                float shadowAlpha = MathHelper.Lerp(1, 0, ZDefGame.lightMap.GetLightLevel() - 0.3f);

                if (ZDefGame.lightMap.GetLightLevel() > 0.7f)
                {
                    shadowAlpha /= 2.0f;
                }

                float shadRotation = (float)Math.Atan2(humanPos.Y - v.Y, humanPos.X - v.X);
                Color shadowColour = new Color(1.0f, 1.0f, 1.0f, shadowAlpha);

                spriteBatch.Draw(ZDefGame.shadowTexture, Position, null, shadowColour, shadRotation, new Vector2(0.0f, 16.0f), 1.0f, SpriteEffects.None, ZDefGame.HUMAN_DRAW_DEPTH + 0.1f);

                if (shadowCount == 5)
                {
                    break;
                }
            }

            spriteBatch.Draw(Texture, Position, null, ZDefGame.lightMap.GetLightColour((int)CurrentTile.TilePos().X, (int)CurrentTile.TilePos().Y), Rotation, Origin, Scale, SpriteEffects.None, ZDefGame.HUMAN_DRAW_DEPTH);
        }

    }
}
