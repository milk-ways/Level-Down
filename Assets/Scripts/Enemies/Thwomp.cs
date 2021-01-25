using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : EnemyController
{
    void Start()
    {
        InvokeRepeating("Move", 1, 1);
    }

    void Update()
    {
        
    }

    void Move()
    {
        rigidBody.gravityScale *= -1;
    }
}
