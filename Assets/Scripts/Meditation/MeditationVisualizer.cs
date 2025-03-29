using UnityEngine;

public class MeditationVisualizer : MonoBehaviour
{
    public GameObject meditationObject; 

    [Header("Visualizer Settings")]
    public int numSamples = 512;        
    public float scaleMultiplier = 50f;  
    public float smoothingSpeed = 2f;
    
    // Abstract scaling using an AnimationCurve
    public AnimationCurve abstractScaleCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1.5f);
    // Color gradient to cycle through colors (set up in the Inspector with calming colors)
    public Gradient colorGradient;
    // Speed at which the gradient cycles independently of the audio amplitude
    public float waveSpeed = 0.3f;   

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

        // Optionally initialize a default gradient if none is set in the Inspector.
        if (colorGradient == null)
        {
            colorGradient = new Gradient();
            GradientColorKey[] colorKey = new GradientColorKey[2];
            colorKey[0].color = Color.white;
            colorKey[0].time = 0.0f;
            colorKey[1].color = Color.red;
            colorKey[1].time = 1.0f;
            GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 1.0f;
            alphaKey[1].time = 1.0f;
            colorGradient.SetKeys(colorKey, alphaKey);
        }
    }

    void Update()
    {
        // Reacquire the meditationSource if necessary
        if (meditationSource == null && meditationObject != null)
        {
            Meditation meditationScript = meditationObject.GetComponent<Meditation>();
            if (meditationScript != null)
            {
                meditationSource = meditationScript.MeditationSource;
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
            
            // Use the audio amplitude to drive the shape (scale)
            float inputValue = average * scaleMultiplier * 10f; // Adjust multiplier as necessary
            float curveValue = abstractScaleCurve.Evaluate(inputValue);
            smoothedScaleFactor = Mathf.Lerp(smoothedScaleFactor, curveValue, smoothingSpeed * Time.deltaTime);
            transform.localScale = visualizerInitialScale * smoothedScaleFactor;

            // Independently cycle the color over time using PingPong
            float t = Mathf.PingPong(Time.time * waveSpeed, 1f);
            Color newColor = colorGradient.Evaluate(t);
            spriteRenderer.color = newColor;
        }
    }
}
