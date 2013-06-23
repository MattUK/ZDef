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
            spriteBatch.Draw(Texture, Position, null, ZDefGame.lightMap.GetLightColour((int)CurrentTile.TilePos().X, (int)CurrentTile.TilePos().Y), Rotation, Origin, Scale, SpriteEffects.None, ZDefGame.HUMAN_DRAW_DEPTH);
        }

    }
}
