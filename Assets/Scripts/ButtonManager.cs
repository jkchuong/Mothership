using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private GameSession gameSession;
    [SerializeField] private TextMeshProUGUI englishSettings;
    [SerializeField] private TextMeshProUGUI japaneseSettings;
    
    public void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Start()
    {
        englishSettings.color = Color.white;
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

    public void LoadHowToPlay()
    {
        Debug.LogError("How to play scene not implemented");
    }

    public void LoadCreators()
    {
        Debug.LogError("Creators and Credits not implemented");
    }

}
