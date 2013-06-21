using System;
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
                BUILDING
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

        public Building(int initialHealth, int initialConstructionState)
        {
            this.Health = initialHealth;
            this.ConstructionState = initialConstructionState;

            Dead = false;
        }

        public abstract TileType GetTileType();

        public abstract bool SpawnAt(TileMap map, int x, int y);

        // To change tile type, do:
        // map.Set<Size>Tile(x, y, new Tile(x, y, <Type>);
        public virtual void Update(TileMap map, int x, int y)
        {
            if (Health <= 0)
            {
                Dead = true;
            }

            if (Dead)
            {
                map.ClearBuilding(x, y);
            }
        }

        public abstract void Draw(Tile tile);

        public abstract bool OnUserInteract(EntityInteraction interaction);
    }
}
