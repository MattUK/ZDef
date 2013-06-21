using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameBase
{
    class Projectile : Sprite
    {
        public int Speed;
        public int Damage;
        public bool Active;

        public Projectile(Vector2 Pos, float Rot, int Dam, Texture2D Tex, int speed)
        {
            Position = Pos;
            Rotation = Rot;
            Texture = Tex;
            Active = true;
            Speed = speed;
            Damage = Dam;
            ConstructThings();
        }

        public void Update()
        {
            if (Active == true)
            {
                Move(Speed);
            }
        }
    }
}
