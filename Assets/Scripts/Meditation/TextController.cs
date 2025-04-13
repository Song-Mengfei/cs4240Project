using System.Collections;
using UnityEngine;
using TMPro; 

public class TMPTextFadeOut : MonoBehaviour
{
    public TMP_Text closeEyesNotif;
    public float delay = 5f;
    public float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeOutAfterDelay());
    }

    private IEnumerator FadeOutAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Color originalColor = closeEyesNotif.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);
            closeEyesNotif.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        closeEyesNotif.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
