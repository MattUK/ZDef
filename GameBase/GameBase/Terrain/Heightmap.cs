using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameBase.Terrain
{
    class Heightmap
    {
        private float[,] values;
        private int width, height;

        private PerlinNoise2D noiseGenerator;

        public Heightmap(int width, int height)
        {
            values = new float[width, height];
            this.width = width;
            this.height = height;

            noiseGenerator = new PerlinNoise2D(width * height * DateTime.Now.Millisecond);

            Console.WriteLine(width * height * DateTime.Now.Millisecond);

            noiseGenerator.setFrequency(1.5f);
            noiseGenerator.setOctaveCount(1);

            GenerateGradientCircle();
            GenerateHeightmap();
        }

        private float[,] GenerateCircle(int circleSize)
        {
            int centreX = circleSize / 2;
            int centreY = circleSize / 2;

            float[,] circleValues = new float[circleSize, circleSize];

            for (int i = 0; i < circleSize; i++)
            {
                for (int j = 0; j < circleSize; j++)
                {
                    float distanceX = (centreX - i) * (centreX - i);
                    float distanceY = (centreY - j) * (centreY - j);

                    float distance = (float)Math.Sqrt(distanceX + distanceY);
                    distance = distance / circleSize;

                    circleValues[i, j] = distance;
                }
            }

            return circleValues;
        }

        private void GenerateGradientCircle()
        {
            float[,] circle = GenerateCircle(width);

            int circleWidth =  width;
            int circleHeight = width;

            int circleX = 0;
            int circleY = 0;

            for (int i = 0; i < circleWidth; i++)
            {
                for (int j = 0; j < circleHeight; j++)
                {
                    values[circleX + i, circleY + j] = circle[i, j];
                }
            }
        }

        private void GenerateHeightmap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float noiseValue = noiseGenerator.generateNoiseValue((float)i / (float)width, (float)j / (float)height) + 1;
                    noiseValue = (float)MathHelper.Clamp(noiseValue, 0.0f, 1.0f);
                    noiseValue = noiseValue - values[i, j];
                    values[i, j] = noiseValue;
                }
            }
        }

        public float GetValueAt(int x, int y)
        {
            return values[x, y];
        }

        public TileType GetTypeAt(int x, int y)
        {
            // 0 < x < 0.5 = Water
            // 0.5 <= x < 0.9 = Grass
            // 0.9 <= x <= 1 = Stone
            float noiseHeight = values[x, y];

            Random rand = new Random((int)(noiseHeight * 100.0f));

            if (noiseHeight < 0.5)
            {
                return TileType.WATER;
            }
            else if (noiseHeight >= 0.5 && noiseHeight < 0.55f)
            {
                return TileType.SAND;
            }
            else
            {
                float val = noiseHeight - (float)rand.NextDouble();
                if (val > -0.1f)
                {
                    return TileType.GRASS;
                }
                else
                {
                    return TileType.STONE;
                }
            }
        }

    }
}
