using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionBoard : MonoBehaviour
{
    [System.Serializable]
    public class InstructionStep
    {
        public string instructionTitle; // Title of the step
        public string instructionText; // Instruction text
        public Sprite poseImage; // Image for the pose
    }

    public InstructionStep[] steps; // Array of steps
    public Image poseImageUI;
    public TMP_Text instructionTextUI;
    public TMP_Text instructionTitleUI;
    public Button nextButton;
    public Button startButton;

    public GameObject lessonPanel;
    public GameObject poseModel;

    private int currentStep = 0;
    public ProgressBarFiller progressBarFiller;
    

    void Start()
    {
        startButton.gameObject.SetActive(false); // Hide start button initially
        HideOtherPanels(); 
        UpdateInstruction();

        startButton.onClick.AddListener(StartLesson);
    }

    public void NextStep()
{
    if (currentStep >= steps.Length - 1) 
    {
        nextButton.gameObject.SetActive(false); // Hide Next button
        startButton.gameObject.SetActive(true); // Show Start button
        return; // Exit function to prevent out-of-bounds issue
    }

    currentStep++; 
    UpdateInstruction();

    // After updating, check if it's the last step and switch buttons
    if (currentStep == steps.Length - 1) 
    {
        nextButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }
}


    void UpdateInstruction()
    {
        instructionTitleUI.text = steps[currentStep].instructionTitle;
        instructionTextUI.text = steps[currentStep].instructionText;
        poseImageUI.sprite = steps[currentStep].poseImage;
    }

    public void StartLesson()
    {
        gameObject.SetActive(false); // Hide instruction panel
        ShowOtherPanels(); 

        if (progressBarFiller != null)
        {
            progressBarFiller.StartLesson();
        }
    }

    void HideOtherPanels()
    {
        if (lessonPanel != null) lessonPanel.SetActive(false);
        poseModel.SetActive(false);

    }

    void ShowOtherPanels()
{
    if (lessonPanel != null) lessonPanel.SetActive(true);
    if (poseModel != null) poseModel.SetActive(true);
}
}

