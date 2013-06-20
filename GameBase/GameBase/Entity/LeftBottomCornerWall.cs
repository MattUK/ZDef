using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    class LeftBottomCornerWall : Building
    {

        public LeftBottomCornerWall(int health, int constructionState)
            : base(health, constructionState)
        {

        }

        public override TileType GetTileType()
        {
            return TileType.WALL_CORNER_LEFT_BOTTOM;
        }

        public override bool SpawnAt(TileMap map, int x, int y)
        {
            if (map.IsBuildingAt(x, y))
            {
                return false;
            }
            else
            {
                map.CreateSmallBuildingAt(x, y, this);
                return true;
            }
        }

        public override void Update(TileMap map, int x, int y)
        {
            throw new NotImplementedException();
        }

    }
}
