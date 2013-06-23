using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace GameBase.Terrain
{
    public class LightMap
    {
        public const float MINIMUM_LIGHT_LEVEL = 0.4f;
        public const float MAXIMUM_LIGHT_LEVEL = 1.0f;
        public const float LIGHT_STEP = 0.01f;

        private List<Vector2> lights;

        private float[,] lightValues;
        private int width, height;

        private float defaultLightValue;
        private bool lightDecreasing;

        private int TimerMax = 15;
        private int Timer = 0;

        public LightMap(int width, int height, float defaultLightValue)
        {
            lightValues = new float[width, height];

            this.width = width;
            this.height = height;

            this.defaultLightValue = defaultLightValue;
            lights = new List<Vector2>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    lightValues[i, j] = -1;
                }
            }
        }

        public Vector2 GetLight(int index)
        {
            return lights[index];
        }
        
        public int GetLightCount()
        {
            return lights.Count;
        }

        public void SetLightValue(float defaultValue)
        {
            defaultLightValue = defaultValue;
        }

        public void AddPointLight(int smallTileX, int smallTileY, float value)
        {
            int x = (int)Math.Floor(smallTileX / 2.0f);
            int y = (int)Math.Floor(smallTileY / 2.0f);
            
            lights.Add(new Vector2(x, y));

            bool above = (y > 0);
            bool below = (y < height - 1);
            bool left = (x > 0);
            bool right = (x < width - 1);
            
            // Spread light to other tiles
            lightValues[x, y] = value;
            if (right) lightValues[x + 1, y] = ((lightValues[x + 1, y] != -1) ? lightValues[x + 1, y] : 0) + value / 1.5f;
            if (left) lightValues[x - 1, y] = ((lightValues[x - 1, y] != -1) ? lightValues[x - 1, y] : 0) + value / 1.5f;
            if (below) lightValues[x, y + 1] = ((lightValues[x, y + 1] != -1) ? lightValues[x, y + 1] : 0) + value / 1.5f;
            if (above) lightValues[x, y - 1] = ((lightValues[x, y - 1] != -1) ? lightValues[x, y - 1] : 0) + value / 1.5f;
            if (right && below) lightValues[x + 1, y + 1] = ((lightValues[x + 1, y + 1] != -1) ? lightValues[x + 1, y + 1] : 0) + value / 1.8f;
            if (left && above) lightValues[x - 1, y - 1] = ((lightValues[x - 1, y - 1] != -1) ? lightValues[x - 1, y - 1] : 0) + value / 1.8f;
            if (left && below) lightValues[x - 1, y + 1] = ((lightValues[x - 1, y + 1] != -1) ? lightValues[x - 1, y + 1] : 0) + value / 1.8f;
            if (right && above) lightValues[x + 1, y - 1] = ((lightValues[x + 1, y - 1] != -1) ? lightValues[x + 1, y - 1] : 0) + value / 1.8f;
        }

        public Color GetLightColour(int smallX, int smallY)
        {
            float value = lightValues[(int)Math.Floor(smallX / 2.0f), (int)Math.Floor(smallY / 2.0f)];
            if (value == -1 || defaultLightValue > value)
            {
                value = defaultLightValue;
            }

            Color white = Color.White;
            white *= value;
            white.A = 255;
            return white;
        }

        public float GetLightLevel()
        {
            return defaultLightValue;
        }

        public float GetLightLevel(int smallX, int smallY)
        {
            float value = lightValues[(int)Math.Floor(smallX / 2.0f), (int)Math.Floor(smallY / 2.0f)];
            if (value == -1 || defaultLightValue > value)
            {
                value = defaultLightValue;
            }

            return value;
        }

        public void Update()
        {
            if (ZDefGame.input.KeyDown(Microsoft.Xna.Framework.Input.Keys.RightShift))
            {
                CycleLighting();
            }

            // UpdateForClockCycle(System.DateTime.Now.Millisecond / 100);

            if (Timer == 0)
            {
                CycleLighting();
                Timer = TimerMax;
            }

            if (Timer > 0)
            {
                Timer--;
            }
        }



        public void UpdateForClockCycle(int time)
        {
            int modTime = time % 100; // Get the time of the actual day (0-100)
            float incrementPerTick = (MAXIMUM_LIGHT_LEVEL - MINIMUM_LIGHT_LEVEL) / 100;
            float calculatedLightLevel = incrementPerTick * modTime;
            defaultLightValue = calculatedLightLevel;
        }

        public void CycleLighting()
        {
            if (defaultLightValue >= MAXIMUM_LIGHT_LEVEL)
            {
                lightDecreasing = true;
            }
            if (defaultLightValue <= MINIMUM_LIGHT_LEVEL)
            {
                lightDecreasing = false;
            }

            if (lightDecreasing)
            {
                DecreaseGlobalLightLevel();
            }
            else
            {
                IncreaseGlobalLightLevel();
            }
        }

        public void IncreaseGlobalLightLevel()
        {
            defaultLightValue += (defaultLightValue < MAXIMUM_LIGHT_LEVEL) ? LIGHT_STEP : 0.0f;
        }

        public void DecreaseGlobalLightLevel()
        {
            defaultLightValue -= (defaultLightValue > MINIMUM_LIGHT_LEVEL) ? LIGHT_STEP : 0.0f;
        }

    }
}
