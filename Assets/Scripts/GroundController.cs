using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    float waitTime = .5f;   // Time for resetting surfaceArc

    PlatformEffector2D platformEffector;

    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (platformEffector.surfaceArc != 360f)
        {
            if (waitTime <= 0)
            {
                platformEffector.surfaceArc = 360f; // Reset surfaceArc
                waitTime = .5f;                     // Reset waitTime
            }
            else
            {
                waitTime -= Time.deltaTime;         // waitTime counter
            }
        }
    }
}
