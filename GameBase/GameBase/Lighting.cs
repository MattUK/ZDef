using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBase
{
    public class Lighting
    {

        public const float MINIMUM_LIGHT_LEVEL = 0.4f;
        public const float MAXIMUM_LIGHT_LEVEL = 1.0f;
        public const float LIGHT_STEP = 0.01f;

        private float globalLightLevel;
        private bool lightDecreasing;

        public Lighting(float initialLightLevel)
        {
            this.globalLightLevel = initialLightLevel;
        }

        public void Update()
        {
            if (ZDefGame.input.KeyDown(Microsoft.Xna.Framework.Input.Keys.RightShift)) 
            {
                CycleLighting();
            }
        }

        public void UpdateForClockCycle(int time)
        {
            int modTime = time % 100; // Get the time of the actual day (0-100)
            float incrementPerTick = (MAXIMUM_LIGHT_LEVEL - MINIMUM_LIGHT_LEVEL) / 100;
            float calculatedLightLevel = incrementPerTick * modTime;
            globalLightLevel = calculatedLightLevel;
        }

        public void CycleLighting()
        {
            if (globalLightLevel >= MAXIMUM_LIGHT_LEVEL)
            {
                lightDecreasing = true;
            }
            if (globalLightLevel <= MINIMUM_LIGHT_LEVEL)
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
            globalLightLevel += (globalLightLevel < MAXIMUM_LIGHT_LEVEL) ? LIGHT_STEP : 0.0f;
        }

        public void DecreaseGlobalLightLevel()
        {
            globalLightLevel -= (globalLightLevel > MINIMUM_LIGHT_LEVEL) ? LIGHT_STEP : 0.0f;
        }

        public float GetLightLevel()
        {
            Console.WriteLine(globalLightLevel);
            return globalLightLevel;
        }

    }
}
