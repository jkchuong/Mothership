using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnScreenExit : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
