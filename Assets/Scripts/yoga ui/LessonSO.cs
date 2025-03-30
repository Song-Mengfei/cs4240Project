using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LessonScriptableObject", order = 1)]
public class LessonSO : ScriptableObject
{
    public InstructionsSO[] instructionsSOs;
}
