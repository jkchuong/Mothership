using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] bool looping = false;
    [SerializeField] GameObject[] Objects;
    [SerializeField] float randomRange = 5f;
    [SerializeField] float delayBeforeSpawning = 20f;
    [SerializeField] float timeBetweenSpawning = 15f;

    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeSpawning);
        do
        {
            yield return StartCoroutine(SpawnPowerUps());
        }
        while (looping);
    }
    
    private IEnumerator SpawnPowerUps()
    {
        Instantiate(Objects[UnityEngine.Random.Range(0, Objects.Length)], transform.position + (transform.right * RandomGen(randomRange)), Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenSpawning);
    }

    private float RandomGen(float randomRange)
    {
        return UnityEngine.Random.Range(-randomRange, randomRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(randomRange*2, 1));
    }
}
