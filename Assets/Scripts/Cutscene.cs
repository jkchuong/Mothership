using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private List<Sprite> sceneImages;

    private SpriteRenderer spriteScene;
    private MusicPlayer musicPlayer;
    
    private void Awake()
    {
        spriteScene = GetComponent<SpriteRenderer>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    private IEnumerator Start()
    {
        musicPlayer.StopMusic();
        
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();

        foreach (Sprite sceneImage in sceneImages)
        {
            spriteScene.sprite = sceneImage;
           
            yield return new WaitForSeconds(5f);
        }
        
        sceneLoader.LoadGameOver();
    }
}
