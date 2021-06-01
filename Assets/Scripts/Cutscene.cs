using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private List<Sprite> sceneImages;

    private SpriteRenderer spriteScene;

    private void Awake()
    {
        spriteScene = GetComponent<SpriteRenderer>();
    }

    private IEnumerator Start()
    {
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();

        foreach (Sprite sceneImage in sceneImages)
        {
            spriteScene.sprite = sceneImage;
           
            yield return new WaitForSeconds(5f);
        }
        
        sceneLoader.LoadGameOver();
    }
}
