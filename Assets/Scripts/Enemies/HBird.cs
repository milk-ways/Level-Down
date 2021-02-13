using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBird : EnemyController
{
    [Header("Movement")]
    public float downSpeed;
    public float downTime;

    [Header("Attack")]
    public float attackSpeed;
}
