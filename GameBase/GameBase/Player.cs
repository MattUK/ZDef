using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBase.Entity;
using GameBase.GUI;

namespace GameBase
{
    class Player
    {
        public const int INITIAL_RESOURCE_COUNT = 100;
        public const int INITIAL_DAY_TIME = 0;

        public static Menu currentMenu;
        public static bool playing;

        public static TileMap tileMap;
        public static List<Human> humanList;

        private int dayTime;
        private int lastTimeIncrement;

        private int resources;

        public Player()
        {
            dayTime = INITIAL_DAY_TIME;
            resources = INITIAL_RESOURCE_COUNT;
        }

        public void SetupGame(int mapWidth, int mapHeight)
        {
            tileMap = new TileMap(mapWidth, mapHeight);
            humanList = new List<Human>();
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

    }
}
