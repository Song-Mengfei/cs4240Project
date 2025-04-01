using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class OurSceneManager : SingletonPatternPersistent<OurSceneManager>
{
    public SceneTransitionFader fader;

    public void LoadMainScene()
    {
        LoadSceneAsync("MainScene");
    }

    public void LoadMindfullnessScene()
    {
        LoadSceneAsync("MindfulnessScene");
    }
    public void LoadYogaScene()
    {
        LoadSceneAsync("YogaScene");
    }

    public void LoadSceneAsync(string sceneStr)
    {
        fader = Camera.main.GetComponentInChildren<SceneTransitionFader>();
        StartCoroutine(GoToSceneAsyncCoroutine(sceneStr));
    }

    IEnumerator GoToSceneAsyncCoroutine(string sceneStr)
    {
        fader.FadeOut();

        // Start loading new scene
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneStr);
        op.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fader.fadeDuration && !op.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        op.allowSceneActivation = true;
        yield return new WaitForSeconds(0.1f);

        fader = Camera.main.GetComponentInChildren<SceneTransitionFader>();
        fader.FadeIn();
    }
}
