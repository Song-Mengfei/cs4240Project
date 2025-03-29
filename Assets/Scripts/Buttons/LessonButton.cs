using UnityEngine;

public class LessonButton : MonoBehaviour
{
    public void OnPressed(int _lessonNumber)
    {
        UserStatsManager.Instance.SetCurrLessonNumber(_lessonNumber);
        OurSceneManager.Instance.LoadYogaScene();
    }
}
