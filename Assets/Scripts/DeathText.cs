using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathText : MonoBehaviour
{
    [SerializeField]
    private string[] deathTexts;

    private TextMeshProUGUI message;

    private void Awake()
    {
        message = GetComponent<TextMeshProUGUI>();
    }

    public void HideMessage()
    {
        message.text = string.Empty;
    }

    public void ShowMessage()
    {
        System.Random rnd = new System.Random();
        message.text = deathTexts[rnd.Next(0, deathTexts.Length)];
    }

    internal bool HasMessage()
    {
        return !string.IsNullOrEmpty(message.text);
    }
}
