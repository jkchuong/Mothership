using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fader : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1 || AudioListener.volume > 0)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            AudioListener.volume -= Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0 || AudioListener.volume < 1)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            AudioListener.volume += Time.deltaTime / time;
            yield return null;
        }
    }

    public void FadeOutImmediate()
    {
        canvasGroup.alpha = 1;
        AudioListener.volume = 0;
    }

    public void FadeInImmediate()
    {
        canvasGroup.alpha = 0;
        AudioListener.volume = 1;
    }
}

