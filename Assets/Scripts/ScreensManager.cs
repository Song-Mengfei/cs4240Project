using TMPro;
using UnityEngine;

public class ScreensManager : SingletonPattern<ScreensManager>
{
    public GameObject StartScreen;
    public GameObject LessonSelectScreen;
    public GameObject EnvironmentSelectScreen;

    public void Start()
    {
        SetAllActive(false);
        StartScreen_SetActive(true);
    }

    public void StartScreen_SetActive(bool _active)
    {
        StartScreen.SetActive(_active);
    }
    public void LessonSelectScreen_SetActive(bool _active)
    {
        LessonSelectScreen.SetActive(_active);
    }
    public void EnvironmentSelectScreen_SetActive(bool _active)
    {
        EnvironmentSelectScreen.SetActive(_active);
    }
    public void SetAllActive(bool _active)
    {
        StartScreen.SetActive(_active);
        LessonSelectScreen.SetActive(_active);
        EnvironmentSelectScreen.SetActive(_active);
    }
}
