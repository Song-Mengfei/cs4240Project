using UnityEngine;
using UnityEngine.UI;

public class EnvironmentManager : SingletonPatternPersistent<EnvironmentManager>
{
    public GameObject[] prefabs;
    public Transform spawnPos;
    int _starter_env_index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (i == _starter_env_index)
            {
                UserStatsManager.Instance.SetCurrEnvironmentNumber(_starter_env_index);
                prefabs[_starter_env_index].SetActive(true);
            }
            else {
                prefabs[i].SetActive(false);
                Debug.Log("Trying to deactivate: " + prefabs[i].name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchEnv(int index, int oldIndex)
    {
        // switch off
        Debug.Log("Trying to deactivate: " + prefabs[oldIndex].name);
        prefabs[oldIndex].SetActive(false);
        prefabs[index].SetActive(true);

    }
}
