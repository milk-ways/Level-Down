using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public float returnTime;
    [SerializeField] float returnTimer;
    bool changed = false;

    PlatformEffector2D ground;

    void Start()
    {
        ground = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (changed)
        {
            if (returnTimer <= 0)
            {
                changed = false;
                ground.surfaceArc = 360;
                returnTimer = returnTime;
            }
            else
            {
                returnTimer -= Time.deltaTime;
            }

        }
    }

    public void ChangeArc()
    {
        changed = true;
        ground.surfaceArc = 180;
        returnTimer = returnTime;
    }
}
