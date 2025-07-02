using TMPro;
using UnityEngine;

public class ScreensManager : SingletonPattern<ScreensManager>
{
    public GameObject StartScreen;
    public GameObject LessonSelectScreen;
    public GameObject EnvironmentSelectScreen;
    public GameObject ModeSelectScreen;
    public GameObject PoseSelectScreen;

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
    public void ModeSelectScreen_SetActive(bool _active)
    {
        ModeSelectScreen.SetActive(_active);
    }
    public void PoseSelectScreen_SetActive(bool _active)
    {
        PoseSelectScreen.SetActive(_active);
    }
    public void SetAllActive(bool _active)
    {
        StartScreen.SetActive(_active);
        ModeSelectScreen.SetActive(_active);
        EnvironmentSelectScreen.SetActive(_active);
        LessonSelectScreen.SetActive(_active);
        PoseSelectScreen.SetActive(_active);
    }
}
