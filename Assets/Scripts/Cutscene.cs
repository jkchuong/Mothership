using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    private IEnumerator Start()
    {
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();

        yield return new WaitForSeconds(10f);

        sceneLoader.LoadGameOver();
    }
}
