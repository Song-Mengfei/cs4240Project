using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionBoard : MonoBehaviour
{
    public LessonSO[] lessonSOs; // Array of lesson scriptable objects
    [SerializeField]
    private int currentStep = 0;
    [SerializeField]
    private int currLessonPose = 0;
    [SerializeField]
    private int currLessonNum = 0;
    private bool isStartOfLesson = true;

    // Instructions related variables
    public GameObject instructionElements;
    public TMP_Text instructionTextUI;
    public TMP_Text instructionTitleUI;
    public TMP_Text nextButtonTextUI;

    // Lesson related variables
    public GameObject[] poseModelRef;
    public Transform poseSpawnTransform;
    public GameObject lessonElements;
    public Image poseImageUI;
    public ProgressBarFiller progressBarFiller;

    // Confirmation screen related variables
    public GameObject ConfirmationElements;

    // Pause related
    public GameObject pauseUI;
    public GameObject pauseButton;
    public GameObject currActiveNonPauseUI; // To set active when unpause

    void PreLoadPoseModels()
    {
        int size = lessonSOs[currLessonNum].PoseSOs.Length;
        poseModelRef = new GameObject[size];

        for (int i = 0; i < size; i++)
        {
            poseModelRef[i] = Instantiate(lessonSOs[currLessonNum].PoseSOs[i].lessonPoseModelPrefab, poseSpawnTransform.position, Quaternion.identity, poseSpawnTransform);
            poseModelRef[i].SetActive(false);
        }
    }

    void Start()
    {
        HideAll();
        ShowInstructionsUI();

        if (UserStatsManager.Instance == null)
        {
            currLessonNum = 0;
        }
        else
        {
            currLessonNum = UserStatsManager.Instance.GetCurrLessonNumber();
        }   

        // Preload all the pose model gos and then set active when using
        PreLoadPoseModels();

        // Fill up text in UI
        UpdateInstruction();
        UpdateLesson();
        UpodateChecker();
        nextButtonTextUI.text = "Next";

        // Get reference to progress bar
        progressBarFiller.ib = GetComponent<InstructionBoard>();
    }

    public void NextStep()
    {
        if (isStartOfLesson)
        {
            isStartOfLesson = false;

            UpdateInstruction();
            return;
        }

        if (currentStep >= lessonSOs[currLessonNum].PoseSOs[currLessonPose].instructionsSOs.Length - 1)
        {
            StartLesson();
            return; // Exit function to prevent out-of-bounds issue
        }

        currentStep++; 
        UpdateInstruction();

        //After updating, check if it's the last step and switch buttons
        if (currentStep == lessonSOs[currLessonNum].PoseSOs[currLessonPose].instructionsSOs.Length - 1)
        {
            nextButtonTextUI.text = "Start";
        }
    }


    void UpdateInstruction()
    {
        if (isStartOfLesson)
        {
            instructionTitleUI.text = "Lesson " + (currLessonNum + 1);
            instructionTextUI.text = lessonSOs[currLessonNum].introString;
            poseImageUI.sprite = lessonSOs[currLessonNum].PoseSOs[currLessonPose].instructionsSOs[currentStep].poseImage;
        }
        else
        {
            instructionTitleUI.text = lessonSOs[currLessonNum].PoseSOs[currLessonPose].instructionsSOs[currentStep].instructionTitle;
            instructionTextUI.text = lessonSOs[currLessonNum].PoseSOs[currLessonPose].instructionsSOs[currentStep].instructionText;
            poseImageUI.sprite = lessonSOs[currLessonNum].PoseSOs[currLessonPose].instructionsSOs[currentStep].poseImage;
        }
    }
    void UpdateLesson()
    {
        if (currLessonPose - 1 >= 0)
        {
            poseModelRef[currLessonPose - 1].SetActive(false);
        }

        poseModelRef[currLessonPose].SetActive(true);
    }

    void UpodateChecker()
    {
        PoseManager.Instance.SetCurrPose(lessonSOs[currLessonNum].PoseSOs[currLessonPose].pose);
    }

    public void StartLesson()
    {
        HideAll();
        ShowLessonUI(); 

        if (progressBarFiller != null)
        {
            progressBarFiller.StartLesson(lessonSOs[currLessonNum].PoseSOs[currLessonPose].poseDurationInSeconds);
        }
    }

    void HideAll()
    {
        lessonElements.SetActive(false);
        instructionElements.SetActive(false);
        ConfirmationElements.SetActive(false);
        pauseUI.SetActive(false);
        pauseButton.SetActive(true);
    }

    void ShowInstructionsUI()
    {
        instructionElements.SetActive(true);
        currActiveNonPauseUI = instructionElements;
    }

    void ShowLessonUI()
    {
        lessonElements.SetActive(true);
        currActiveNonPauseUI = lessonElements;
    }

    void ShowConfirmationUI()
    {
        ConfirmationElements.SetActive(true);
        currActiveNonPauseUI = ConfirmationElements;
    }

    public void FullyFilled()
    {
        HideAll();

        // Check if it's the last pose
        if (currLessonPose == lessonSOs[currLessonNum].PoseSOs.Length - 1)
        {
            ShowConfirmationUI();
        }
        else
        {
            currLessonPose++;
            currentStep = 0;

            nextButtonTextUI.text = "Next";

            UpdateInstruction();
            UpdateLesson();
            UpodateChecker();
            ShowInstructionsUI();
        }
    }

    public void GoToMeditationScene()
    {
        OurSceneManager.Instance.LoadMindfullnessScene();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseUI.SetActive(true);
        pauseButton.SetActive(false);
        currActiveNonPauseUI.SetActive(false);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseUI.SetActive(false);
        pauseButton.SetActive(true);
        currActiveNonPauseUI.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        OurSceneManager.Instance.LoadMainScene();
    }
}

