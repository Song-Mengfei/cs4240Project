using UnityEngine;

public class EnvironmentButton : MonoBehaviour
{
    public void OnPressed(int _environmentNumber)
    {
        UserStatsManager.Instance.SetCurrEnvironmentNumber(_environmentNumber);
    }
}
