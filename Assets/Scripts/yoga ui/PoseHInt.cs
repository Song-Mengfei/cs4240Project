using UnityEngine;
using TMPro;

public class PoseHInt : MonoBehaviour
{
    private TMP_Text poseStatTextUI;
    private TMP_Text poseHintTitleUI;
    private PoseManager poseManager;

    void Start()
    {
        Transform child1 = transform.Find("Pose reminder");
        Transform child2 = transform.Find("Pose hint");

        poseStatTextUI = child1.GetComponent<TMP_Text>();
        poseHintTitleUI = child2.GetComponent<TMP_Text>();

        poseManager = PoseManager.Instance;
    }

    void Update()
    {
        poseStatTextUI.text = poseManager.GetPoseStat();
        poseHintTitleUI.text = poseManager.GetPoseHint();
    }

}
