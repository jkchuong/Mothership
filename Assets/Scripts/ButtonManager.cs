using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    SceneLoader sceneLoader;
    GameSession gameSession;

    public void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
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
    }

    public void SetLanguageJapanese()
    {
        gameSession.language = "ja";
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

}
