using UnityEngine;
using UnityEngine.UI;

public class EnvironmentButton : MonoBehaviour
{
    public int lessonToCompleteToUnlock;
    public int environmentNumber;
    public Text unlockText;
    public void OnPressed(int _environmentNumber)
    {
        // int _latestEnvironmentUnlocked = UserStatsManager.Instance.GetlatestEnvironmentUnlocked();
        // if (_environmentNumber > _latestEnvironmentUnlocked)
        // {
        //     Debug.Log("Environment " + _environmentNumber + " is not unlocked yet.");
        //     return;
        // }
        EnvironmentManager.Instance.SwitchEnv(_environmentNumber, UserStatsManager.Instance.GetCurrEnvironmentNumber());
        
        Debug.Log("Trying to update env num: " + _environmentNumber);
        UserStatsManager.Instance.SetCurrEnvironmentNumber(_environmentNumber);
        Debug.Log("Updated env num: " + UserStatsManager.Instance.GetCurrEnvironmentNumber());
    }

    public void OnPressed()
    {
        // If the environment is not unlocked yet
        if (lessonToCompleteToUnlock > UserStatsManager.Instance.GetlatestLessonUnlocked() - 1)
        {
            return;
        }

        EnvironmentManager.Instance.SwitchEnv(environmentNumber, UserStatsManager.Instance.GetCurrEnvironmentNumber());

        Debug.Log("Trying to update env num: " + environmentNumber);
        UserStatsManager.Instance.SetCurrEnvironmentNumber(environmentNumber);
        Debug.Log("Updated env num: " + UserStatsManager.Instance.GetCurrEnvironmentNumber());
    }

    private void Start()
    {
        // If the environment is not unlocked yet
        if (lessonToCompleteToUnlock > UserStatsManager.Instance.GetlatestLessonUnlocked() - 1)
        {
            return;
        }

        unlockText.text = "";
    }
}
