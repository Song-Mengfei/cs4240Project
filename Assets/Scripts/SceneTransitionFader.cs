using System.Collections;
using UnityEngine;

public class SceneTransitionFader : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2.0f;
    public Color fadeColor;
    private Renderer r;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<Renderer>();

        // Make it in front of the camera
        transform.position += new Vector3(0, 0, Camera.main.nearClipPlane + 0.005f);

        if (fadeOnStart)
            FadeIn();
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeCoroutine(alphaIn, alphaOut));
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }
    public void FadeOut()
    {
        Fade(0, 1);
    }

    public IEnumerator FadeCoroutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            r.material.SetColor("_BaseColor", newColor);

            timer += Time.deltaTime;
            yield return null;
        }


        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        r.material.SetColor("_BaseColor", newColor2);
    }
}
