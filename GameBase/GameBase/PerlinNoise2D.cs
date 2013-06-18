using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBase
{
    /// <summary>
    /// Custom perlin noise implementation, uses positions on a circle instead of stupid complex 3D cube line gradient things.
    /// </summary>
    public class PerlinNoise2D
    {

        private const int PERMUTATION_SIZE = 256;

        private static float[][] gradients = new float[][] {
            new float[] {(float)Math.Cos(90), (float)Math.Sin(90)},
            new float[] {(float)Math.Cos(60), (float)Math.Sin(60)},
            new float[] {(float)Math.Cos(45), (float)Math.Sin(45)},
            new float[] {(float)Math.Cos(30), (float)Math.Sin(30)},
            new float[] {(float)Math.Cos(0), (float)Math.Sin(0)},
            new float[] {(float)Math.Cos(330), (float)Math.Sin(330)},
            new float[] {(float)Math.Cos(315), (float)Math.Sin(315)},
            new float[] {(float)Math.Cos(300), (float)Math.Sin(300)},
            new float[] {(float)Math.Cos(270), (float)Math.Sin(270)},
            new float[] {(float)Math.Cos(240), (float)Math.Sin(240)},
            new float[] {(float)Math.Cos(225), (float)Math.Sin(225)},
            new float[] {(float)Math.Cos(210), (float)Math.Sin(210)},
            new float[] {(float)Math.Cos(180), (float)Math.Sin(180)},
            new float[] {(float)Math.Cos(150), (float)Math.Sin(150)},
            new float[] {(float)Math.Cos(135), (float)Math.Sin(135)},
            new float[] {(float)Math.Cos(120), (float)Math.Sin(120)}
        };

        private int seed;
        private int[] permutations;
        private int octaveCount;
        private float lacunarity;
        private float persistence;
        private float frequency;

        public PerlinNoise2D(int seed)
        {
            this.seed = seed;
            this.persistence = 0.5f;
            this.lacunarity = 2.0f;
            this.octaveCount = 6;
            this.frequency = 1.0f;

            // Create permutation table
            this.permutations = new int[PERMUTATION_SIZE];

            for (int i = 0; i < PERMUTATION_SIZE; i++)
            {
                this.permutations[i] = i;
            }

            shufflePermutations();
        }

        private void shufflePermutations()
        {
            // Randomly shuffle permutation table
            Random r = new Random(seed);
            for (int i = 0; i < PERMUTATION_SIZE; i++)
            {
                int newPosition = r.Next(PERMUTATION_SIZE - 1);
                int oldValue = this.permutations[newPosition];
                this.permutations[newPosition] = this.permutations[i];
                this.permutations[i] = oldValue;
            }
        }

        private int getPermutation(int x)
        {
            return this.permutations[x & (PERMUTATION_SIZE - 1)];
        }

        private float[] getGradient(int x, int y)
        {
            int lookup = getPermutation(x + getPermutation(y));

            return gradients[lookup % 16];
        }

        private float dot(float[] x, float[] y)
        {
            return x[0] * y[0] + x[1] * y[1];
        }

        private float[] sub(float[] x, float[] y)
        {
            return new float[] { x[0] - y[0], x[1] - y[1] };
        }

        private float lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        private float getNoiseValue(float x, float y)
        {
            int floorX = (int)Math.Floor(x);
            int floorY = (int)Math.Floor(y);

            float[] topLeft = getGradient(floorX, floorY);
            float[] topRight = getGradient(floorX + 1, floorY);
            float[] bottomLeft = getGradient(floorX, floorY + 1);
            float[] bottomRight = getGradient(floorX + 1, floorY + 1);

            float[] noisePosition = new float[] { x, y };

            float s = dot(noisePosition, topLeft);
            float t = dot(noisePosition, topRight);
            float u = dot(noisePosition, bottomLeft);
            float v = dot(noisePosition, bottomRight);

            float fadeX = 6 * (float)Math.Pow(x - floorX, 5) - 15 * (float)Math.Pow(x - floorX, 4) + 10 * (float)Math.Pow(x - floorX, 3);
            float fadeY = 6 * (float)Math.Pow(y - floorY, 5) - 15 * (float)Math.Pow(y - floorY, 4) + 10 * (float)Math.Pow(y - floorY, 3);

            float a = lerp(s, t, fadeX);
            float b = lerp(u, v, fadeX);

            float c = lerp(a, b, fadeY);

            return c;
        }

        public int getSeed()
        {
            return this.seed;
        }

        public int getOctaveCount()
        {
            return this.octaveCount;
        }

        public void setOctaveCount(int octaveCount)
        {
            this.octaveCount = octaveCount;
        }

        public float getLacunarity()
        {
            return lacunarity;
        }

        public void setLacunarity(float lacunarity)
        {
            this.lacunarity = lacunarity;
        }

        public float getPersistence()
        {
            return persistence;
        }

        public void setPersistence(float persistence)
        {
            this.persistence = persistence;
        }

        public void setFrequency(float frequency)
        {
            this.frequency = frequency;
        }

        public float getFrequency()
        {
            return frequency;
        }

        /// <summary>
        /// Generates a noise value, then composes it with a fractal using Brownian Motion.
        /// </summary>
        /// <param name="x">The 'x' position in the noise field.</param>
        /// <param name="y">The 'y' position in the noise field.</param>
        /// <returns>A pseudo-randomly generated, procedural noise value.</returns>
        public float generateNoiseValue(float x, float y)
        {
            float total = 0.0f;
            float localFrequency = frequency;
            float amplitude = persistence;

            for (int i = 0; i < octaveCount; i++)
            {
                total += getNoiseValue(x * localFrequency, y * localFrequency) * amplitude;
                localFrequency *= lacunarity;
                amplitude *= persistence;
            }

            return total;
        }

    }
}
