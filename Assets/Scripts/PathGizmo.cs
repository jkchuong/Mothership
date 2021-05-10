using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; i++)
        {
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.3f);

            if (i + 1 < children)
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }
}
