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

        public void Update()
        {
            Move(Speed);
        }
    }
}
