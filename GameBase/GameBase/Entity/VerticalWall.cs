using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    public class VerticalWall : Building
    {

        public VerticalWall(int health, int constructionState)
            : base(health, constructionState)
        {

        }

        public override TileType GetTileType()
        {
            return TileType.WALL_VERTICAL;
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
            // Wall to left and wall below = CORNER_LEFT_BOTTOM
            // Wall to right and wall below = CORNER_RIGHT_BOTTOM
            // Wall to left and wall above = CORNER_TOP_LEFT
            // Wall to right and wall above = CORNER_TOP_RIGHT
            
        }

    }
}
