using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingBoard : MonoBehaviour
{
    public SettingSO setting;
    [SerializeField]
    private int currentStep = 0;
    [SerializeField]
    private ArmCalibration armCalibration;

    // Settings related variables
    public GameObject settingElements;
    public TMP_Text settingTextUI;
    public TMP_Text settingTitleUI;
    public TMP_Text nextButtonTextUI;
    public TMP_Text confirmationTextUI;

    public Image settingImageUI;

    // Confirmation screen related variables
    public GameObject ConfirmationElements;
    public GameObject finalElements;

    void Start()
    {
        HideAll();
        nextButtonTextUI.text = "Next";
        armCalibration = GetComponent<ArmCalibration>();
        UpdateInstruction();
    }

    public void NextStep(bool isNext)
    {
        if (currentStep > setting.instructionsSOs.Length - 1)
        {
            ShowFinalUI();
            return; // Exit function to prevent out-of-bounds issue
        }

        if (isNext)
        {
            armCalibration.recordValue(currentStep);
            currentStep++;
        }

        UpdateInstruction();

        //After updating, check if it's the last step and switch buttons
        if (currentStep == setting.instructionsSOs.Length - 1)
        {
            armCalibration.startMeasure();
        }
        else {
            armCalibration.startRecord();
        }
    }


    void UpdateInstruction()
    {
        settingTitleUI.text = setting.instructionsSOs[currentStep].instructionTitle;
        settingTextUI.text = setting.instructionsSOs[currentStep].instructionText;
        settingImageUI.sprite = setting.instructionsSOs[currentStep].poseImage;
        ShowSettingUI();
    }

    void HideAll()
    {
        settingElements.SetActive(false);
        finalElements.SetActive(false);
        ConfirmationElements.SetActive(false);
    }

    void ShowFinalUI()
    {
        HideAll();
        finalElements.SetActive(true);
    }

    void ShowSettingUI()
    {
        HideAll();
        settingElements.SetActive(true);
    }

    void ShowConfirmationUI(float length)
    {
        if (currentStep != setting.instructionsSOs.Length - 1)
        {
            length *= 100f;
            confirmationTextUI.text = "The data calibrated is: " + length.ToString("F2") + "cm";
        }
        else
        {
            length -= 0.8f; 
            confirmationTextUI.text = "The data calibrated is: " + length.ToString("F2") + "m";
        }


        HideAll();
        ConfirmationElements.SetActive(true);
    }

    public void DataCalibrated(float length)
    {
        HideAll();
        ShowConfirmationUI(length);
    }
    public void GoToMainScene()
    {
        OurSceneManager.Instance.LoadMainScene();
    }
}
