using UnityEngine;

public class EnvironmentButton : MonoBehaviour
{
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
}
