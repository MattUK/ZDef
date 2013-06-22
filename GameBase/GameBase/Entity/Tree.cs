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
            //Console.WriteLine(Dead);

            if (Health <= 0)
            {
                Dead = true;
            }
            if (Dead == true)
            {
               // Console.WriteLine(Health);
                map.SetSmallTile(x, y, new Tile(x, y, TileType.TREE_STUMP));
            }
        }


        public override void Draw(Tile tile)
        {
            //tile.Draw();
            Color drawColour = Color.White * ZDefGame.lighting.GetLightLevel();
            drawColour.A = 255;
            ZDefGame.spriteBatch.Draw(ZDefGame.humanBuildingTexture, tile.GetRectangle(), tile.GetTileType().GetSourcePositionRectangle(), drawColour, 0.0f, new Vector2(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, ZDefGame.BUILDING_DRAW_DEPTH);
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
