using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    public class WatchTower : Building
    {

        public bool occupied;
        public Sprite user;

        public WatchTower(int health, int constructionState)
            : base(health, constructionState)
        {
            Large = true;
        }

        public override TileType GetTileType()
        {
            return TileType.WOODEN_WATCH_TOWER;
        }

        public override bool SpawnAt(TileMap map, int x, int y)
        {
            int largeX = (int)Math.Floor(x / 2.0f);
            int largeY = (int)Math.Floor(y / 2.0f);

            int alignedSmallX = largeX * 2;
            int alignedSmallY = largeY * 2;

            if (map.IsBuildingAt(alignedSmallX, alignedSmallY) || map.IsBuildingAt(alignedSmallX + 1, alignedSmallY) || map.IsBuildingAt(alignedSmallX + 1, alignedSmallY + 1) || map.IsBuildingAt(alignedSmallX, alignedSmallY + 1))
            {
                return false;
            }
            else
            {
                return map.CreateLargeBuildingAt(x, y, this);
            }
        }

        public override string GetName()
        {
            return "WATCHTOWER";
        }

        public override void Update(TileMap map, int x, int y)
        {

        }

        public void Evacuate()
        {
            if (occupied)
            {
                ZDefGame.HumanList.Add((Human)user);
            }
        }

        public override bool OnUserInteract(Building.EntityInteraction interaction)
        {
            if (interaction.interaction == EntityInteraction.Interaction.DESTROYING)
            {
                Health -= interaction.modifier;
                return true;
            }
            else if (interaction.interaction == EntityInteraction.Interaction.BUILDING)
            {
                Health += interaction.modifier;
                return true;
            }
            else if (interaction.interaction == EntityInteraction.Interaction.USING)
            {
                occupied = true;
                user = interaction.cause;
                ZDefGame.HumanList.Remove((Human)user);
                return true;
            }
            return false;
        }

        public override void Draw(Tile tile, int smallTileX, int smallTileY)
        {
            base.Draw(tile, smallTileX, smallTileY);
        }
    }
}
