﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    public abstract class Building : Sprite
    {
        public int Health;
        public int ConstructionState;

        public Building(int initialHealth, int initialConstructionState)
        {
            this.Health = initialHealth;
            this.ConstructionState = initialConstructionState;
        }

        public abstract TileType GetTileType();

        public abstract bool SpawnAt(TileMap map, int x, int y);

        public abstract void Update(TileMap map, int x, int y);

        //public abstract void Draw(Tile tile);
    }
}
