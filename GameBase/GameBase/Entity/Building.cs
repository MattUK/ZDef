﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Terrain;

namespace GameBase.Entity
{
    public abstract class Building : Sprite
    {

        public class EntityInteraction
        {
            public enum Interaction
            {
                DESTROYING,
                BUILDING,
                USING
            }

            public Interaction interaction;
            public Sprite cause;
            public int modifier;

            public EntityInteraction(Interaction interaction, Sprite cause, int modifier)
            {
                this.interaction = interaction;
                this.cause = cause;
                this.modifier = modifier;
            }
        }

        public int Health;
        public int ConstructionState;
        public bool Built;
        public bool Large;

        public Building(int initialHealth, int initialConstructionState)
        {
            this.Health = initialHealth;
            this.ConstructionState = initialConstructionState;

            Dead = false;
        }

        public abstract TileType GetTileType();

        public abstract bool SpawnAt(TileMap map, int x, int y);

        public abstract String GetName();

        // To change tile type, do:
        // map.Set<Size>Tile(x, y, new Tile(x, y, <Type>);
        public abstract void Update(TileMap map, int x, int y);

        public virtual void Draw(Tile tile, int smallTileX, int smallTileY)
        {
            tile.Draw(smallTileX, smallTileY);
        }

        public abstract bool OnUserInteract(EntityInteraction interaction);
    }
}
