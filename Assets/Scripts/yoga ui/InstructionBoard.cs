using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionBoard : MonoBehaviour
{
    public LessonSO[] lessonSOs; // Array of lesson scriptable objects
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
        if (currentStep >= lessonSOs.Length - 1)
        {
            nextButton.gameObject.SetActive(false); // Hide Next button
            startButton.gameObject.SetActive(true); // Show Start button
            return; // Exit function to prevent out-of-bounds issue
        }

        currentStep++; 
        UpdateInstruction();

        //After updating, check if it's the last step and switch buttons
        if (currentStep == lessonSOs.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
    }


    void UpdateInstruction()
    {
        int currLessonNum = UserStatsManager.Instance.GetCurrLessonNumber();
        instructionTitleUI.text = lessonSOs[currLessonNum].instructionsSOs[currentStep].instructionTitle;
        instructionTextUI.text = lessonSOs[currLessonNum].instructionsSOs[currentStep].instructionText;
        poseImageUI.sprite = lessonSOs[currLessonNum].instructionsSOs[currentStep].poseImage;
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

