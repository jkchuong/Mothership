using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private GameSession gameSession;
    private MusicPlayer musicPlayer;
    [SerializeField] private TextMeshProUGUI englishSettings;
    [SerializeField] private TextMeshProUGUI japaneseSettings;
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject howToPlayCanvas;

    public void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    private void Start()
    {
        if (englishSettings)
        {
            englishSettings.color = Color.white;
        }

        if (musicPlayer)
        {
            musicPlayer.PlayMusic();
        }
    }

    public void LoadStartScene()
    {
        sceneLoader.LoadStartMenu();
    }

    public void LoadGameScene()
    {
        sceneLoader.LoadGameScene();
    }

    public void LoadKillScene()
    {
        sceneLoader.LoadKillScene();
    }

    public void LoadMissScene()
    {
        sceneLoader.LoadMissScene();
    }

    public void LoadGameOver()
    {
        sceneLoader.LoadGameOver();
    }

    public void SetLanguageEnglish()
    {
        gameSession.language = "";
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        englishSettings.color = Color.white;
        japaneseSettings.color = Color.yellow;
    }

    public void SetLanguageJapanese()
    {
        gameSession.language = "ja";
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        japaneseSettings.color = Color.white;
        englishSettings.color = Color.yellow;
    }

    public void LoadCreators()
    {
        creditsCanvas.SetActive(true);
        startCanvas.SetActive(false);
        howToPlayCanvas.SetActive(false);
    }

    public void LoadStart()
    {
        creditsCanvas.SetActive(false);
        startCanvas.SetActive(true);
        howToPlayCanvas.SetActive(false);
    }

    public void LoadHowTo()
    {
        creditsCanvas.SetActive(false);
        startCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }

}
