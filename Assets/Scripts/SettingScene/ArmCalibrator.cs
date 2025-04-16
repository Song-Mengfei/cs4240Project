using UnityEngine;
using UnityEngine.InputSystem;

public class ArmCalibration : MonoBehaviour
{
    public GameObject LeftController;
    public GameObject RightController;
    public GameObject head;
    public InputActionReference recordAction;
    public InputActionReference measureAction;

    private SettingBoard settingBoard;
    private float distance;

    private void Start()
    {
        recordAction.action.performed += OnRecordPormed;
        measureAction.action.performed += OnMeasurePormed;
        settingBoard = GetComponent<SettingBoard>();
    }

    public void startRecord()
    {
        recordAction.action.Enable();
    }

    public void startMeasure()
    {
        measureAction.action.Enable();
    }

    void finishRecord()
    {
        recordAction.action.Disable();
        measureAction.action.Disable();
    }

    void OnRecordPormed(InputAction.CallbackContext ctx)
    {
        distance = Vector3.Distance(LeftController.transform.position, RightController.transform.position);

        finishRecord();
        settingBoard.DataCalibrated(distance);
    }

    void OnMeasurePormed(InputAction.CallbackContext ctx)
    {
        distance = head.transform.position.y;

        finishRecord();
        settingBoard.DataCalibrated(distance);
    }


    public void recordValue(int step)
    {
        switch (step) { 
            case 0:
                PlayerPrefs.SetFloat("LeftForearmLength", distance);
                PlayerPrefs.Save();
                break;
            case 1:
                PlayerPrefs.SetFloat("RightForearmLength", distance);
                PlayerPrefs.Save();
                break;
            case 2:
                PlayerPrefs.SetFloat("ShoulderLength", distance);
                PlayerPrefs.Save();
                break;
            case 3:
                PlayerPrefs.SetFloat("ArmSpan", distance);
                PlayerPrefs.Save();
                break;
            case 4:
                PlayerPrefs.SetFloat("ChestHeight", distance);
                PlayerPrefs.Save();
                break;
            case 5:
                PlayerPrefs.SetFloat("UpperbodyLength", distance);
                PlayerPrefs.Save();
                break;
            case 6:
                PlayerPrefs.SetFloat("ThighLength", distance);
                PlayerPrefs.Save();
                break;
            case 7:
                PlayerPrefs.SetFloat("HeadHeight", distance);
                PlayerPrefs.Save();
                break;
        }
    }
}
