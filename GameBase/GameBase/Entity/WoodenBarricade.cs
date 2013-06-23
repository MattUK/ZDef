using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    class WoodenBarricade : Building
    {
        public WoodenBarricade(int health, int constructionState)
            : base(health, constructionState)
        {

        }

        public override TileType GetTileType()
        {
            return TileType.WOODEN_BARRICADE;
        }

        public override string GetName()
        {
            return "WOODEN BARRICADE";
        }

        public override bool SpawnAt(TileMap map, int x, int y)
        {
            if (map.IsBuildingAt(x, y))
            {
                return false;
            }
            else
            {
                return map.CreateSmallBuildingAt(x, y, this);
            }
        }

        public override void Update(TileMap map, int x, int y)
        {
                   
        }

        public override bool OnUserInteract(Building.EntityInteraction interaction)
        {
            throw new NotImplementedException();
        }

    }
}
