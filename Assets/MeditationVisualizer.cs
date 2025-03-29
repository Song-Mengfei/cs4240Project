using UnityEngine;

public class MeditationVisualizer : MonoBehaviour
{

    public GameObject meditationObject; 

    [Header("Visualizer Settings")]
    public int numSamples = 512;        
    public float scaleMultiplier = 50f;  
    public float smoothingSpeed = 2f;
    public Color lowAmplitudeColor = Color.white;
    public Color highAmplitudeColor = Color.red;
    private AudioSource meditationSource;
    private float[] spectrumData;

    private Vector3 visualizerInitialScale;

    private float smoothedScaleFactor = 1f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        visualizerInitialScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (meditationObject != null)
        {
            Meditation meditationScript = meditationObject.GetComponent<Meditation>();
            if (meditationScript != null)
            {
                meditationSource = meditationScript.MeditationSource;
            }
            else
            {
                Debug.LogError("No Meditation component found on the assigned GameObject!");
            }
        }
        else
        {
            Debug.LogError("Please assign the Meditation GameObject in the Inspector.");
        }
        spectrumData = new float[numSamples];
    }

    void Update()
    {
        if(meditationSource == null)
        {
            if(meditationObject != null)
            {
                Meditation meditationScript = meditationObject.GetComponent<Meditation>();
                if(meditationScript != null)
                {
                    meditationSource = meditationScript.MeditationSource;
                }
            }
        }
        
        if (meditationSource != null && meditationSource.isPlaying)
        {
            meditationSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
            float sum = 0f;
            for (int i = 0; i < numSamples; i++)
            {
                sum += spectrumData[i];
            }
            float average = sum / numSamples;
            float scaleFactor = 1 + average * scaleMultiplier;
            smoothedScaleFactor = Mathf.Lerp(
                smoothedScaleFactor, 
                scaleFactor, 
                smoothingSpeed * Time.deltaTime
            );
            transform.localScale = visualizerInitialScale * smoothedScaleFactor;

            float colorBlendValue = Mathf.Clamp01(average * scaleMultiplier);
            Color newColor = Color.Lerp(lowAmplitudeColor, highAmplitudeColor, colorBlendValue);
            if (spriteRenderer != null)
            {
                spriteRenderer.color = newColor;
            }
        }
    }

}
