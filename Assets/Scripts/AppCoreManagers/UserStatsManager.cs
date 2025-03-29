using UnityEngine;

public class UserStatsManager : SingletonPatternPersistent<UserStatsManager>
{
    private int currLessonNumber;
    public int GetCurrLessonNumber() { return currLessonNumber; }
    public bool SetCurrLessonNumber(int _currLessonNumber) 
    {
        if (_currLessonNumber > latestLessonCompleted)
        {
            return false;
        }
        currLessonNumber = _currLessonNumber;
        return true;
    }
    private int latestLessonCompleted;
    public int GetlatestLessonCompleted() { return latestLessonCompleted; }
    public void SetlatestLessonCompleted(int _latestLessonCompleted) { latestLessonCompleted = _latestLessonCompleted; }

    private int currEnvironmentNumber;
    public int GetCurrEnvironmentNumber() { return currEnvironmentNumber; }
    public bool SetCurrEnvironmentNumber(int _currEnvironmentNumber)
    {
        if (_currEnvironmentNumber > latestEnvironmentUnlocked)
        {
            return false;
        }
        currLessonNumber = _currEnvironmentNumber;
        return true;
    }
    private int latestEnvironmentUnlocked;
    public int GetlatestEnvironmentUnlocked() { return latestEnvironmentUnlocked; }
    public void SetlatestEnvironmentUnlocked(int _latestEnvironmentUnlocked) { latestEnvironmentUnlocked = _latestEnvironmentUnlocked; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
