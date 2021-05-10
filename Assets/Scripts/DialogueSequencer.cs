using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequencer : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeConversationStart;

    [SerializeField]
    private GameObject[] conversations;

    [SerializeField]
    private float[] conversationDelay;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeBeforeConversationStart);

        for (int i = 0; i < conversations.Length; i++)
        {
            conversations[i].SetActive(true);
            yield return new WaitForSeconds(conversationDelay[i]);
            conversations[i].SetActive(false);
        }

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.StopEnemySpawning();
    }
}
