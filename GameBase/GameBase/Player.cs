using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBase
{
    class Player
    {
        public const int INITIAL_RESOURCE_COUNT = 100;
        public const int INITIAL_DAY_TIME = 0;

        private int dayTime;
        private int resources;

        public Player()
        {
            dayTime = INITIAL_DAY_TIME;
            resources = INITIAL_RESOURCE_COUNT;
        }

        public int GetResourceCount()
        {
            return resources;
        }

        public int GetTime()
        {
            return dayTime;
        }

        public void Update()
        {

        }

    }
}
