using UnityEngine;
using System.Collections;

public class Meditation : MonoBehaviour
{
    public AudioClip meditationClip;
    public AudioClip bgmClip;

    public float meditationDelay = 10f;
    
    private AudioSource bgmSource;
    private AudioSource meditationSource;

    public NightModeSwitcher nightModeSwitcher;
    public float bgmDefaultVolume = 0.5f;
    public float bgmLoweredVolume = 0.2f;
    public float volumeFadeSpeed = 0.6f;
    
    void Start()
    {
        nightModeSwitcher = gameObject.GetComponent<NightModeSwitcher>();
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true; 
        bgmSource.volume = bgmDefaultVolume; 
        bgmSource.Play();
        
        meditationSource = gameObject.AddComponent<AudioSource>();
        meditationSource.clip = meditationClip;
        meditationSource.loop = false;  
        
        StartCoroutine(PlayMeditationAfterDelay(meditationDelay));

        if (nightModeSwitcher != null)
        {
            nightModeSwitcher.SwitchToNightMode();
        }
        else 
        {
            Debug.LogWarning("NightModeSwitcher reference is not set in the Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bgmSource.isPlaying || meditationSource.isPlaying)
            {
                bgmSource.Pause();
                meditationSource.Pause();
            }
            else
            {
                bgmSource.UnPause();
                meditationSource.UnPause();
            }
        }

        if (meditationSource != null)
        {
            if (meditationSource.isPlaying)
            {
                bgmSource.volume = Mathf.Lerp(bgmSource.volume, bgmLoweredVolume, volumeFadeSpeed * Time.deltaTime);
            }
            else
            {
                bgmSource.volume = Mathf.Lerp(bgmSource.volume, bgmDefaultVolume, volumeFadeSpeed * Time.deltaTime);
            }
        }
    }

    
    IEnumerator PlayMeditationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        meditationSource.Play();
        StartCoroutine(FadeOutBGMAfterMeditation(2f));
    }

    IEnumerator FadeOutBGMAfterMeditation(float fadeDuration)
    {
        // Wait until the meditation clip is finished
        yield return new WaitWhile(() => meditationSource.isPlaying);

        float startVolume = bgmSource.volume;
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        bgmSource.volume = 0f;
        bgmSource.Stop();
    }
    public AudioSource MeditationSource
    {
        get { return meditationSource; }
    }

}
