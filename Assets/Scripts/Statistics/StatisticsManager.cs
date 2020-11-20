﻿using Common;

namespace Statistics
{
    public class StatisticsManager : Singleton<StatisticsManager>
    {
        private int currentLevel = 1;

        private int currentExperience;
        // TODO: Some skills 

        public void AddExperience(int amount)
        {
            currentExperience += amount;
        }

        public int GetExperience()
        {
            return currentExperience;
        }
    }
}