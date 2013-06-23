using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameBase.Entity
{
    public class Weapon
    {
        public int Damage;
        public int Delay;
        int DelayMax;
        public Vector2 Position;
        public float Rotation;
        public Sprite Target; //placeholder
        Texture2D BulletTex;
        //List<Projectile> BulletList;

        public Weapon(Vector2 Pos, float Rot, int delay, int Dam, Texture2D BulTex)
        {
            Position = Pos;
            Rotation = Rot;
            Delay = delay;
            DelayMax = Delay;
            Damage = Dam;
            BulletTex = BulTex;
           // BulletList = new List<Projectile>();
        }

        public void Update()
        {
            if (Target != null)
            {
                float XDis = (Position.X - Target.Position.X);
                float YDis = (Position.Y - Target.Position.Y);

                Rotation = (float)Math.Atan2(-YDis, -XDis);

                if (Delay == 0)
                {
                    Fire();
                    Delay = DelayMax;
                }


            }

            //for (int i = 0; i < BulletList.Count; i++)
            //{
            //    BulletList[i].Update();
            //}


            if (Delay > 0)
            {
                Delay -= 1;
            }
        }

        public void Fire()
        {
            Projectile Bullet = new Projectile(Position, Rotation, Damage, BulletTex, 5);
            ZDefGame.BulletList.Add(Bullet);
            //BulletList.Add(Bullet);
        }
    }
}
