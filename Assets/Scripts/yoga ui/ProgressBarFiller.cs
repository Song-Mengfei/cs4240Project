using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarFiller : MonoBehaviour
{
    public Image progressFill; // Assign ProgressFill Image in Inspector
    public float fillSpeed = 0.5f; // Speed at which the bar fills
    public float decreaseSpeed = 0.3f;
    [SerializeField] private bool canFill = false;
    public bool isProgressFullyFilled = false;

    private PositionManager positionManager;
    public InstructionBoard ib;

    void Start()
    {
        progressFill.fillAmount = 0f; // Start at 0
        positionManager = PositionManager.Instance;
    }

    void Update()
    {
        if (!canFill) return; // Only update if lesson has started
        if (isProgressFullyFilled) return; // No need to do anything anymore once fully filled

        if (positionManager.IsPoseCorrect())
        {
            progressFill.fillAmount = Mathf.Clamp01(progressFill.fillAmount + Time.deltaTime * fillSpeed);

            if (progressFill.fillAmount == 1)
            {
                isProgressFullyFilled = true;
                ib.FullyFilled();
            }
        }
        else
        {
            Debug.Log("not correct");
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
}

