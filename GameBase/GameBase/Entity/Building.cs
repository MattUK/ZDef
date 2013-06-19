using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBase.Entity
{
    public abstract class Building : Sprite
    {
        public int Health;
        public int constructionStatus;

        public abstract void SpawnAt(TileMap map, int x, int y);
    }
}
