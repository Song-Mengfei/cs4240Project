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
    
    void Start()
    {
        nightModeSwitcher = gameObject.GetComponent<NightModeSwitcher>();
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;  
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
    
    IEnumerator PlayMeditationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        meditationSource.Play();
    }
    public AudioSource MeditationSource
    {
        get { return meditationSource; }
    }
}
