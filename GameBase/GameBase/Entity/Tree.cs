using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;
using Microsoft.Xna.Framework;

namespace GameBase.Entity
{
    class Tree : Building
    {
        public Tree() : base (100, 100)
        {
            Health = 50;
            Dead = false;
            Built = true;
            Large = false;
        }

        public override TileType GetTileType()
        {
            return TileType.TREE;
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

        public override string GetName()
        {
            return "TREE";
        }

        public override void Update(TileMap map, int x, int y)
        {
            if (Health <= 0)
            {
                Dead = true;
            }
            if (Dead == true)
            {
                map.SetSmallTile(x, y, new Tile(x, y, TileType.TREE_STUMP));
            }
        }

        public override bool OnUserInteract(Building.EntityInteraction interaction)
        {
            if (interaction.interaction == EntityInteraction.Interaction.DESTROYING)
            {
                Health -= interaction.modifier;
                return true;
            }
            return false;
        }

    }
}
