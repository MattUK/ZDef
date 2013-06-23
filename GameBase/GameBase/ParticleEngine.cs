using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameBase
{
    class ParticleEngine
    {
        List<Particle> ParticleList;
        List<Texture2D> TexList;
        Random Rand;
        public Vector2 EmitterLocation;

        int TimeToLiveMax;
        float SizeDivision;

        public Sprite Parent;

        public ParticleEngine(List<Texture2D> TeList, Vector2 Pos, int TTLMAX, float SizeDiv, Sprite Par)
        {
            ParticleList = new List<Particle>();
            TexList = TeList;
            EmitterLocation = Pos;
            Rand = new Random();

            TimeToLiveMax = TTLMAX;
            SizeDivision = SizeDiv;

            Parent = Par;
        }

        public ParticleEngine(Texture2D Tex, Vector2 Pos, int TTLMAX, float SizeDiv, Sprite Par)
        {
            ParticleList = new List<Particle>();
            TexList = new List<Texture2D>();
            TexList.Add(Tex);
            EmitterLocation = Pos;
            Rand = new Random();

            TimeToLiveMax = TTLMAX;
            SizeDivision = SizeDiv;

            Parent = Par;
        }

        Particle GenerateParticle(Color Colour, bool Cloud)
        {
            Texture2D texture = TexList[Rand.Next(TexList.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(1f * (float)(Rand.NextDouble() * 2 - 1), 1f * (float)(Rand.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(Rand.NextDouble() * 2 - 1);
            Color color = new Color((float)Rand.NextDouble(), (float)Rand.NextDouble(), (float)Rand.NextDouble());
            float size = (float)Rand.NextDouble() / SizeDivision; //Nice 'Smokey' effect.  
            //float size = (float)Rand.NextDouble() / 3; //Nice 'Smokey' effect.
            //float size = (float)Rand.NextDouble() / 20; //Nice H:\Sinistar\GameBase\GameBase\GameBase\InputHandler.cs'Line' effect.
            //float size = (float)Rand.NextDouble() / 5; //Nice 'Line' effect, smaller.
            int ttl = Rand.Next(1, TimeToLiveMax);  //5,10 makes for a great looking fighter trail.

            if (Colour == null)
            {
                return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl, Cloud, Parent);
            }
            else
            {
                return new Particle(texture, position, velocity, angle, angularVelocity, Colour, size, ttl, Cloud, Parent);
            }
        }

        public void Update(Color Colour, bool CreateParticles, bool Cloud)
        {
            int total = 40;

            if (CreateParticles == true)
            {
                for (int i = 0; i < total; i++)
                {
                    ParticleList.Add(GenerateParticle(Colour, Cloud));
                }
            }

            for (int i = 0; i < ParticleList.Count; i++)
            {
                ParticleList[i].Update();
                if (ParticleList[i].TimeToLive <= 0)
                {
                    ParticleList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ParticleList.Count; i++)
            {
                ParticleList[i].Draw(spriteBatch);
            }
        }
    }
}
