using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBase.Terrain
{
    public class GradientCircle
    {

        private float[,] valueArray;
        private int width, height;

        public GradientCircle(int width, int height)
        {
            valueArray = new float[width, height];
            this.width = width;
            this.height = height;

            GenerateValues();
        }

        private void GenerateValues()
        {
            int centreX = width / 2;
            int centreY = height / 2;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float distanceX = (centreX - i) * (centreX - i);
                    float distanceY = (centreY - j) * (centreY - j);

                    float distance = (float)Math.Sqrt(distanceX + distanceY);
                    distance = distance / width;

                    valueArray[i, j] = distance;
                }
            }
        }

    }
}
