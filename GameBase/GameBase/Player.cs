using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Entity;
using GameBase.GUI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameBase
{
    public class Player
    {
        public const int INITIAL_RESOURCE_COUNT = 100;
        public const int INITIAL_DAY_TIME = 0;

        private int dayTime;
        private TimeSpan lastTimeIncrement;

        private bool pauseTime;

        private int resources;

        private int waveCount;
        private bool inWave;

        public Player()
        {
            dayTime = INITIAL_DAY_TIME;
            resources = INITIAL_RESOURCE_COUNT;
        }

        public void LoadFromSave(String saveName)
        {

        }

        public void SaveToFile(String saveName)
        {

        }

        public bool InWave()
        {
            return inWave;
        }

        public int GetWaveNumber()
        {
            return waveCount;
        }

        public void PauseTime()
        {
            pauseTime = true;
        }

        public void ResumeTime()
        {
            pauseTime = false;
        }

        public int GetResourceCount()
        {
            return resources;
        }

        public int GetTime()
        {
            return dayTime;
        }

        public void IncrementResourceCount(int amount)
        {
            resources += amount;
        }

        public void Update(GameTime gameTime)
        {
            // Increment current time of day
            if (lastTimeIncrement.Add(new TimeSpan(0, 0, 0, 0, 100)) < gameTime.TotalGameTime)
            {
                dayTime++;
                lastTimeIncrement = gameTime.TotalGameTime;

                ZDefGame.lightMap.UpdateForClockCycle(dayTime);
            }

            // Pause at night
            if (dayTime == 0)
            {
                PauseTime();
                waveCount++;
                inWave = true;
            }
        }

        public void Draw()
        {

        }

    }
}
