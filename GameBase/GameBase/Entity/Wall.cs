using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    class Wall : Building
    {

        public Wall(int health, int constructionState)
            : base(health, constructionState)
        {

        }

        public override Terrain.TileType GetTileType()
        {
            return TileType.WALL_HORIZONTAL;
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
            bool wallAbove = false;
            bool wallBelow = false;
            bool wallLeft = false;
            bool wallRight = false;

            TileType newType = TileType.WALL_HORIZONTAL;

            if (map.GetBuildingAt(x, y - 1) != null)
            {
                wallAbove = map.GetBuildingAt(x, y - 1).GetTileType().tileID == 80;
            }
            if (map.GetBuildingAt(x, y + 1) != null)
            {
                wallBelow = map.GetBuildingAt(x, y + 1).GetTileType().tileID == 80;
            }
            if (map.GetBuildingAt(x + 1, y) != null)
            {
                wallLeft = map.GetBuildingAt(x + 1, y).GetTileType().tileID == 80;
            }
            if (map.GetBuildingAt(x - 1, y) != null)
            {
                wallRight = map.GetBuildingAt(x - 1, y).GetTileType().tileID == 80;
            }

            if (wallAbove || wallBelow && !wallLeft && !wallRight) newType = TileType.WALL_VERTICAL;
            if (wallLeft || wallRight && !wallAbove && !wallBelow) newType = TileType.WALL_HORIZONTAL;
            if (wallAbove && wallRight && !wallBelow && !wallLeft) newType = TileType.WALL_TOP_TO_RIGHT;
            if (wallAbove && wallLeft && !wallBelow && !wallRight) newType = TileType.WALL_TOP_TO_LEFT;
            if (wallBelow && wallLeft && !wallAbove && !wallRight) newType = TileType.WALL_LEFT_TO_BOTTOM;
            if (wallBelow && wallRight && !wallAbove && !wallLeft) newType = TileType.WALL_RIGHT_TO_BOTTOM;
            if (wallAbove && wallBelow && wallRight && !wallLeft) newType = TileType.WALL_VERTICAL_WITH_RIGHT;
            if (wallAbove && wallBelow && wallLeft && !wallRight) newType = TileType.WALL_VERTICAL_WITH_LEFT;
            if (wallLeft && wallRight && wallBelow && !wallAbove) newType = TileType.WALL_HORIZONTAL_WITH_BOTTOM;
            if (wallLeft && wallRight && wallAbove && !wallBelow) newType = TileType.WALL_HORIZONTAL_WITH_TOP;
            if (wallLeft && wallRight && wallAbove && wallBelow) newType = TileType.WALL_ALL_FOUR;

            map.SetSmallTile(x, y, new Tile(x, y, newType));
        }

        public override void Draw(Tile tile)
        {
            tile.Draw();
        }

        public override bool OnUserInteract(Building.EntityInteraction interaction)
        {
            throw new NotImplementedException();
        }

    }
}
