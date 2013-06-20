﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameBase.Entity
{
    class Weapon
    {
        public int Damage;
        public int Delay;
        int DelayMax;
        public Vector2 Position;
        public float Rotation;

        public Weapon(Vector2 Pos, float Rot, int delay, int Dam)
        {
            Position = Pos;
            Rotation = Rot;
            Delay = delay;
            DelayMax = Delay;
            Damage = Dam;
        }

        public void Update()
        {

        }
    }
}