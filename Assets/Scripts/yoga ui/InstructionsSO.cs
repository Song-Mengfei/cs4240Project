using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InstructionsScriptableObject", order = 1)]
public class InstructionsSO : ScriptableObject
{
    public string instructionTitle; // Title of the step
    public string instructionText; // Instruction text
    public Sprite poseImage; // Image for the pose
}
