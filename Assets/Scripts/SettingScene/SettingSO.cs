using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SettingScriptableObject", order = 1)]
public class SettingSO : ScriptableObject
{
    public InstructionsSO[] instructionsSOs;
}
