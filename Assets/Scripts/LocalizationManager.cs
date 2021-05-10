using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private void Start()
    {
        DialogueManager.SetLanguage(GameSession.instance.language);
        DialogueLua.SetVariable("AudioLang", GameSession.instance.language);
    }
}
