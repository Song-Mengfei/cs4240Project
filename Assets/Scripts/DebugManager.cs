using TMPro;
using UnityEngine;

public class DebugManager : SingletonPatternPersistent<DebugManager>
{
    public bool debugON;
    public TextMeshProUGUI debugText;
    public static void Log(string message)
    {
        if (!Instance.debugON)
            return;

        if (Instance.debugText != null)
        {
            int maxLines = 10; // Adjust as needed
            string[] lines = Instance.debugText.text.Split('\n');
            if (lines.Length >= maxLines)
            {
                Instance.debugText.text = string.Join("\n", lines, 1, lines.Length - 1);
            }
            Instance.debugText.text += message + "\n";
        }
        Debug.Log(message);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OurSceneManager.Instance.LoadMainScene();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OurSceneManager.Instance.LoadMindfullnessScene();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OurSceneManager.Instance.LoadYogaScene();
        }
    }
}
