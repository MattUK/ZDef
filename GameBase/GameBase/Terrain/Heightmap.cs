using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            noiseGenerator = new PerlinNoise2D(width * height);
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
            float[,] circle = GenerateCircle(width - (width / 4));

            int circleX = 0, circleY = 0;
            
            for (int i = width / 4; i < width; i++)
            {
                circleX ++;
                for (int j = height / 4; j < height; j++)
                {
                    circleY ++;
                    values[i, j] = circle[circleX, circleY];
                }
            }
        }

        private void GenerateHeightmap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float noiseValue = noiseGenerator.generateNoiseValue(i, j);
                    noiseValue -= values[i, j];
                    values[i, j] = noiseValue;
                }
            }
        }

        public TileType GetTypeAt(int x, int y)
        {
            // 0 < x < 0.5 = Water
            // 0.5 <= x < 0.9 = Grass
            // 0.9 <= x <= 1 = Stone
            float height = values[x, y];

            if (height < 0.5)
            {
                return TileType.WATER;
            }
            else if (height >= 0.5 && height < 0.9)
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
