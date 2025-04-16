using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarFiller : MonoBehaviour
{
    public Image progressFill; // Assign ProgressFill Image in Inspector

    public Image backgroundImage;
    public Color originalColor;
    public Color correctPoseColor;

    //public float fillSpeed = 0.5f; // Speed at which the bar fills (NO LONGER USING SINCE THERES FIXED TIME)
    public float durationInSeconds = 10.0f;
    public float decreaseSpeed = 0.3f;
    [SerializeField] private bool canFill = false;
    [SerializeField] private bool debugFastCompleteBool = false;
    public bool isProgressFullyFilled = false;


    public float testTimeElapsed = 0.0f;

    private PositionManager positionManager;
    public InstructionBoard ib;

    void Start()
    {
        progressFill.fillAmount = 0f; // Start at 0
        positionManager = PositionManager.Instance;
        originalColor = backgroundImage.color;
    }

    void Update()
    {
        testTimeElapsed += Time.deltaTime;
       

        if (!canFill) return; // Only update if lesson has started
        if (isProgressFullyFilled) return; // No need to do anything anymore once fully filled

        if (positionManager.IsPoseCorrect() || debugFastCompleteBool)
        {
            progressFill.fillAmount = Mathf.Clamp01(progressFill.fillAmount + Time.deltaTime / durationInSeconds);
            backgroundImage.color = correctPoseColor;

            if (progressFill.fillAmount == 1)
            {
                backgroundImage.color = originalColor;
                isProgressFullyFilled = true;
                ib.FullyFilled();
            }
        }
        else
        {
            Debug.Log("not correct");
            backgroundImage.color = originalColor;

            progressFill.fillAmount = Mathf.Clamp01(progressFill.fillAmount - Time.deltaTime * decreaseSpeed);
        }
    }

    public void StartLesson()
    {
        // Start filling the progress bar when the lesson starts
        progressFill.fillAmount = 0f;
        canFill = true;
        isProgressFullyFilled = false;
        // IncreaseProgress(1f); // Set the target to fill completely
    }

    public void StartLesson(float _durationInSeconds)
    {
        durationInSeconds = _durationInSeconds;
        StartLesson();
    }
}

