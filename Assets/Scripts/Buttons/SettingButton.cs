using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public void OnPressed()
    {
        OurSceneManager.Instance.LoadSettingScene();
    }
}
