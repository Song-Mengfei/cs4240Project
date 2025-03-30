using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarFiller : MonoBehaviour
{
    public Image progressFill; // Assign ProgressFill Image in Inspector
    public float fillSpeed = 0.5f; // Speed at which the bar fills
    public float decreaseSpeed = 0.3f;
    [SerializeField] private bool canFill = false;

    void Start()
    {
        progressFill.fillAmount = 0f; // Start at 0
    }

    void Update()
    {
        if (!canFill) return; // Only update if lesson has started

        if (PoseManager.Instance.IsPoseCorrect())
        {
            progressFill.fillAmount = Mathf.Clamp01(progressFill.fillAmount + Time.deltaTime * fillSpeed);
        }
        else
        {
            progressFill.fillAmount = Mathf.Clamp01(progressFill.fillAmount - Time.deltaTime * decreaseSpeed);
        }


    }

    public void StartLesson()
    {
        // Start filling the progress bar when the lesson starts
        progressFill.fillAmount = 0f;
        canFill = true;
        // IncreaseProgress(1f); // Set the target to fill completely
    }
}

