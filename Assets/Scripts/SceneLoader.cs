using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance = null;

    [SerializeField] float delayInSeconds = 2f;
    [SerializeField] float faderSpeed = 0.8f;
    [SerializeField] float fadeWaitTime = 2f;

    private Fader fader;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Game Over"));
    }

    public void LoadKillScene()
    {
        StartCoroutine(WaitAndLoad("Kill Scene"));
    }

    public void LoadMissScene()
    {
        StartCoroutine(WaitAndLoad("Miss Scene"));
    }

    private IEnumerator WaitAndLoad(string sceneName)
    {
        fader = FindObjectOfType<Fader>();

        yield return new WaitForSeconds(delayInSeconds);

        yield return fader.FadeOut(faderSpeed);

        yield return SceneManager.LoadSceneAsync(sceneName);

        //Destroy(GameObject.Find("Main Music"));

        yield return new WaitForSeconds(fadeWaitTime);

        yield return fader.FadeIn(faderSpeed);
    }

    private IEnumerator QuickFadeOutSlowFadeIn(string sceneName)
    {
        fader = FindObjectOfType<Fader>();

        yield return fader.FadeOut(0.01f);

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return fader.FadeIn(faderSpeed);
    }

    public void LoadGameScene()
    {
        StartCoroutine(QuickFadeOutSlowFadeIn("Game Scene"));

        //SceneManager.LoadScene("Game Scene");
        //Cursor.visible = true;
    }

    public void LoadStartMenu()
    {
        StartCoroutine(WaitAndLoad("Start Scene"));
    }
}
