using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    #region Singleton
    public static DebugManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        instance.debugText.text = "";
    }
    #endregion

    public bool debugON;
    public TextMeshProUGUI debugText;
    public static void Log(string message)
    {
        if (!instance.debugON)
            return;

        if (instance.debugText != null)
        {
            int maxLines = 5; // Adjust as needed
            string[] lines = instance.debugText.text.Split('\n');
            if (lines.Length >= maxLines)
            {
                instance.debugText.text = string.Join("\n", lines, 1, lines.Length - 1);
            }
            instance.debugText.text += message + "\n";
        }
        Debug.Log(message);
    }
}
