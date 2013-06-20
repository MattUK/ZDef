using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Entity;
using GameBase.GUI;
using Microsoft.Xna.Framework.Graphics;

namespace GameBase
{
    public class Player
    {
        public const int INITIAL_RESOURCE_COUNT = 100;
        public const int INITIAL_DAY_TIME = 0;

        private int dayTime;
        private int lastTimeIncrement;

        private int resources;

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

        public void Update()
        {
            // Increment current time of day
            if (lastTimeIncrement + 1 < Environment.TickCount)
            {
                dayTime++;
            }
        }

        public void Draw()
        {

        }

    }
}
