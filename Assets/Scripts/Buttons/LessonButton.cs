using UnityEngine;

public class LessonButton : MonoBehaviour
{
    public void OnPressed(int _lessonNumber)
    {
        if (UserStatsManager.Instance.SetCurrLessonNumber(_lessonNumber))
        {
            OurSceneManager.Instance.LoadYogaScene();
        }
    }
}
