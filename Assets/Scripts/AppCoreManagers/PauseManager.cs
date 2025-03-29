using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : SingletonPattern<PauseManager>
{
    public GameObject quitPanel;  

    void Start()
    {
        quitPanel.SetActive(false);  // Ensure the panel is hidden at the start
    }

    public void ShowQuitPanel()
    {
        quitPanel.SetActive(true);  // Show the quit confirmation panel
    }

    public void HideQuitPanel()
    {
        quitPanel.SetActive(false);  // Hide the panel
    }

    public void QuitLesson()
    {
        SceneManager.LoadScene("HomeScene");  // Replace with your actual home scene name
    }

    // Pause all audio sources
    void PauseAll()
    {
        AudioListener.pause = true;
    }

    // Resume all audio sources
    void ResumeAll() 
    {
        AudioListener.pause = false;
    }
}

