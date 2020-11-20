using UnityEngine;

public class StatisticsManager : Singleton<StatisticsManager>
{
    private int currentLevel = 1;

    private int currentExperience = 0;
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