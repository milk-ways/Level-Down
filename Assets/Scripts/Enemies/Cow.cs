using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : EnemyController
{
    [Header("Movement")]
    public float normalSpeed;
    public float madSpeed;
    float speed;

    [Header("Attack")]
    public LayerMask playerLayer;
    public Transform sightPos;
    public float sightRadius;
    public float returnNormalTime;          // Time for turning back to normal when player out of sight
    float returnNormalTimer;                // Timer
    bool isMad = false;                     // Mad state when player in sight

    void Update()
    {
        isMad = Physics2D.OverlapCircle(sightPos.position, sightRadius, playerLayer);
    }
}
