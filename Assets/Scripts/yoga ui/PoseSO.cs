using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PoseScriptableObject", order = 1)]
public class PoseSO : ScriptableObject
{
    public InstructionsSO[] instructionsSOs;
    public GameObject lessonPoseModelPrefab;
    public float poseDurationInSeconds;

    public Pose pose;
}
