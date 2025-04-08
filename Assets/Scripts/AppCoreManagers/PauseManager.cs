using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : SingletonPattern<PauseManager>
{
    public GameObject pauseUI;
    public GameObject pauseButton;

    void Start()
    {
        pauseUI.SetActive(false);  // Ensure the PauseUI is hidden at the start
        pauseButton.SetActive(true);
    }

    public void ShowPauseUI()
    {
        PauseAll();
        pauseUI.SetActive(true);  // Show the PauseUI
        pauseButton.SetActive(false);
    }

    public void HidePauseUI()
    {
        ResumeAll();
        pauseUI.SetActive(false);  // Hide the PauseUI
        pauseButton.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        OurSceneManager.Instance.LoadMainScene();
    }

    // Pause all audio sources
    void PauseAll()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    // Resume all audio sources
    void ResumeAll()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}

