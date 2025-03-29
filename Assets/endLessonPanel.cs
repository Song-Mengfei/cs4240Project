using UnityEngine;
using UnityEngine.UI; // For Button functionality

public class EndLessonPanel : MonoBehaviour
{
    public GameObject endLessonPanel; // The panel that appears at the end of the lesson


    void Start()
    {
        // Initially hide the end lesson panel
        endLessonPanel.SetActive(false);
    }

    // Call this method when the lesson ends
    public void ShowEndLessonPanel()
    {
        endLessonPanel.SetActive(true);  // Show the panel
    }

    // Handle Home button click (go to Home scene)
    public void OnHomeButtonClick()
    {
        // Use OurSceneManager to load the Home scene
        OurSceneManager.Instance.LoadMainScene();  // Assuming the Home scene is called "MainScene"
    }

    // Handle Next Lesson button click (go to the Next Lesson scene)
    public void OnNextLessonButtonClick()
    {
        // Use OurSceneManager to load the Next Lesson scene
        OurSceneManager.Instance.LoadMindfullnessScene();  // Assuming the next lesson is called "MindfulnessScene"
    }
}

