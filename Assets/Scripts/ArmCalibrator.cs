using UnityEngine;
using UnityEngine.InputSystem;

public class ArmCalibration : MonoBehaviour
{
    public GameObject otherController;
    public GameObject head;
    public InputActionReference recordAction;

    private Vector3 elbowMarker;
    private Vector3 shoulderMarker;
    private int calibrationStep = 0;

    private void Start()
    {
        recordAction.action.Enable();
        recordAction.action.performed += OnRecordPormed;
    }

    void Update()
    {

    }

    void OnRecordPormed(InputAction.CallbackContext ctx)
    {
        if (calibrationStep == 0)
        {
            recordElbow();
            calibrationStep++;
        }
        else if (calibrationStep == 1)
        {
            recordShoulder();
            calibrationStep++;
        }
        else if (calibrationStep == 2) 
        {
            if (otherController.name == "Left")
            {
                recordOtherShoulder();
            }
            else if (otherController.name == "Right")
            {
                recordHeight();
            }
            calibrationStep++;

        }
    }


    private void recordElbow()
    {
        elbowMarker = transform.position;
        DebugManager.Log("Elbow marked at: " + elbowMarker);

        float forearmLength = Vector3.Distance(elbowMarker, otherController.transform.position);
        string dataName = otherController.name + "ForearmLength";

        DebugManager.Log($"{dataName} is: {forearmLength}");
        PlayerPrefs.SetFloat(dataName, forearmLength);
        PlayerPrefs.Save();
    }

    private void recordShoulder()
    {
        shoulderMarker = transform.position;
        DebugManager.Log("Shoulder marked at: " + shoulderMarker);

        float upperArmLength = Vector3.Distance(elbowMarker, shoulderMarker);
        string dataName = otherController.name + "UpperArmLength";

        DebugManager.Log($"{dataName} is: {upperArmLength}");
        PlayerPrefs.SetFloat(dataName, upperArmLength);
        PlayerPrefs.Save();
    }

    private void recordOtherShoulder() 
    {
        Vector3 otherShoulderMarker = transform.position;
        DebugManager.Log("Shoulder marked at: " + otherShoulderMarker);

        float shoulderLength = Vector3.Distance(otherShoulderMarker, shoulderMarker);

        DebugManager.Log($"shoulder length is: {shoulderLength}");
        PlayerPrefs.SetFloat("ShoulderLength", shoulderLength);
        PlayerPrefs.Save();
    }

    private void recordHeight()
    {
        float headMarker = head.transform.position.y;

        DebugManager.Log($"head height is: {headMarker}");
        PlayerPrefs.SetFloat("HeadHeight", headMarker);
        PlayerPrefs.Save();
    }
}
