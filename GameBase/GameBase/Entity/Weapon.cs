using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameBase.Entity
{
    class Weapon
    {
        public int Damage;
        public int Delay;
        int DelayMax;
        public Vector2 Position;
        public float Rotation;
        Sprite Target;
        Texture2D BulletTex;
        List<Projectile> BulletList;

        public Weapon(Vector2 Pos, float Rot, int delay, int Dam)
        {
            Position = Pos;
            Rotation = Rot;
            Delay = delay;
            DelayMax = Delay;
            Damage = Dam;
            BulletList = new List<Projectile>();
        }

        public void Update()
        {
            if (Target != null && Delay == 0)
            {
                Fire();
            }

            foreach (Projectile bullet in BulletList)
            {
                bullet.Update();
            }


            if (Delay > 0)
            {
                Delay -= 1;
            }
        }

        void Fire()
        {
            Console.WriteLine("A zombie has been shot!");
            Projectile Bullet = new Projectile(Position, Rotation, Damage, BulletTex, 5);

            BulletList.Add(Bullet);
        }
    }
}
