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

    [SerializeField] private float timeToTransitionMother = 3f;

    private bool isLoading = false;
    private float timeNotHit = 100f;
    
    private Animator animator;
    private SceneLoader sceneLoader;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(startAnimationLength);
    }

    private void Update()
    {
        if (isLoading) return;
        
        timeNotHit += Time.deltaTime;
        if (timeNotHit >= timeToTransitionMother)
        {
            animator.SetBool("isGettingHit", false);
        }
        
        timeToStartSceneB -= Time.deltaTime;
        if (timeToStartSceneB <= 0)
        {
            sceneLoader.LoadMissScene();
            isLoading = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Laser"))
        {
            collision.gameObject.SetActive(false);
            animator.SetBool("isGettingHit", true);
            timeNotHit = 0f;

            if (timeToStartSceneB <= 10f)
            {
                timeToStartSceneB = 10f;
            }

            hitsToStartSceneA--;

            if (hitsToStartSceneA <= 0)
            {
                sceneLoader.LoadKillScene();
                isLoading = true;
            }
        }
    }
}
