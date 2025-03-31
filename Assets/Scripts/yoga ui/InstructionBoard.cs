using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionBoard : MonoBehaviour
{
    public LessonSO[] lessonSOs; // Array of lesson scriptable objects
    [SerializeField]
    private int currentStep = 0;
    [SerializeField]
    private int currLessonNum = 0;

    // Instructions related variables
    public GameObject instructionElements;
    public TMP_Text instructionTextUI;
    public TMP_Text instructionTitleUI;
    public TMP_Text nextButtonTextUI;

    // Lesson related variables
    public GameObject poseModel;
    public GameObject lessonElements;
    public Image poseImageUI;
    public ProgressBarFiller progressBarFiller;

    void Start()
    {
        HideAll();
        ShowInstructionsUI();
        currLessonNum = UserStatsManager.Instance.GetCurrLessonNumber();
        Debug.Log(currLessonNum);
        nextButtonTextUI.text = "Next";

        UpdateInstruction();
        UpdateLesson();
    }

    public void NextStep()
    {
        if (currentStep >= lessonSOs[currLessonNum].instructionsSOs.Length - 1)
        {
            StartLesson();
            return; // Exit function to prevent out-of-bounds issue
        }

        currentStep++; 
        UpdateInstruction();

        //After updating, check if it's the last step and switch buttons
        if (currentStep == lessonSOs[currLessonNum].instructionsSOs.Length - 1)
        {
            nextButtonTextUI.text = "Start";
        }
    }


    void UpdateInstruction()
    {
        instructionTitleUI.text = lessonSOs[currLessonNum].instructionsSOs[currentStep].instructionTitle;
        instructionTextUI.text = lessonSOs[currLessonNum].instructionsSOs[currentStep].instructionText;
        poseImageUI.sprite = lessonSOs[currLessonNum].instructionsSOs[currentStep].poseImage;
    }
    void UpdateLesson()
    {
        poseModel = lessonSOs[currLessonNum].lessonPoseModelPrefab;
    }

    public void StartLesson()
    {
        HideAll();
        ShowLessonUI(); 

        if (progressBarFiller != null)
        {
            progressBarFiller.StartLesson();
        }
    }

    void HideAll()
    {
        lessonElements.SetActive(false);
        instructionElements.SetActive(false);

    }

    void ShowInstructionsUI()
    {
        instructionElements.SetActive(true);
    }

    void ShowLessonUI()
    {
        lessonElements.SetActive(true);
    }
}

