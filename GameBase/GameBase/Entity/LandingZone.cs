using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    class LandingZone : Building
    {
        //
        Tile UpperLeft;  //Tiles for the soldiers to spawn on.
        Tile UpperRight;
        Tile LowerLeft;
        Tile LowerRight;

                public LandingZone(int health, int constructionState)
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

        public override string GetName()
        {
            return "LZ";
        }


        public override void Update(TileMap map, int x, int y)
        {

            //Updaty stuff here?
        }

        public override bool OnUserInteract(Building.EntityInteraction interaction)
        {
            return false;
        }

        //I cannot work your codeeeee
        void SpawnSoldiers(bool SpawnEngie)
        {
            ZDefGame.SpawnRifleman(UpperLeft.TilePos().X, UpperLeft.TilePos().Y);
            ZDefGame.SpawnRifleman(UpperRight.TilePos().X, UpperRight.TilePos().Y);
            ZDefGame.SpawnRifleman(LowerLeft.TilePos().X, LowerLeft.TilePos().Y);
            if (SpawnEngie == false)
            {
                ZDefGame.SpawnRifleman(LowerRight.TilePos().X, LowerRight.TilePos().Y);
            }
            else
            {
                ZDefGame.SpawnEngineer(LowerRight.TilePos().X, LowerRight.TilePos().Y);  
            }
        }
    }
}
