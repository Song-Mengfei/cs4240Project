using UnityEngine;

public class UserStatsManager : SingletonPatternPersistent<UserStatsManager>
{
    private int currLessonNumber;
    public int GetCurrLessonNumber() { return currLessonNumber; }
    public bool SetCurrLessonNumber(int _currLessonNumber) 
    {
        if (_currLessonNumber > latestLessonUnlocked)
        {
            return false;
        }
        currLessonNumber = _currLessonNumber;
        return true;
    }
    [SerializeField]
    private int latestLessonUnlocked;
    public int GetlatestLessonUnlocked() { return latestLessonUnlocked; }
    public void SetlatestLessonUnlocked(int _latestLessonUnlocked) { latestLessonUnlocked = _latestLessonUnlocked; }

    private int currEnvironmentNumber;
    public int GetCurrEnvironmentNumber() { return currEnvironmentNumber; }
    public bool SetCurrEnvironmentNumber(int _currEnvironmentNumber)
    {
        // if (_currEnvironmentNumber > latestEnvironmentUnlocked)
        // {
        //     return false;
        // }
        // this will be checked in EnvironmentButton.cs
        currEnvironmentNumber = _currEnvironmentNumber;
        return true;
    }
    private int latestEnvironmentUnlocked;
    public int GetlatestEnvironmentUnlocked() { return latestEnvironmentUnlocked; }
    public void SetlatestEnvironmentUnlocked(int _latestEnvironmentUnlocked) { latestEnvironmentUnlocked = _latestEnvironmentUnlocked; }

    public void CompletedCurrentLesson()
    {
        // If completed the latest lesson, unlock the next lesson and environment
        if (currLessonNumber == latestLessonUnlocked)
        {
            latestLessonUnlocked++;
            latestEnvironmentUnlocked++;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
