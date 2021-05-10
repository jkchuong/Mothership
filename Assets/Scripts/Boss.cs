using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float startAnimationLength = 1f;

    [SerializeField]
    private float hitsToStartSceneA = 50;

    [SerializeField]
    private float timeToStartSceneB = 20f;

    Animator animator;
    SceneLoader sceneLoader;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(startAnimationLength);
        animator.enabled = false;
    }

    private void Update()
    {
        timeToStartSceneB -= Time.deltaTime;
        if (timeToStartSceneB <= 0)
        {
            sceneLoader.LoadMissScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Laser"))
        {
            collision.gameObject.SetActive(false);

            if (timeToStartSceneB <= 10f)
            {
                timeToStartSceneB = 10f;
            }

            hitsToStartSceneA--;

            if (hitsToStartSceneA <= 0)
            {
                sceneLoader.LoadKillScene();
            }
        }
    }
}
