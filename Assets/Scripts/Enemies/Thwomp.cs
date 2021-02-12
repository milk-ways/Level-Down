using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : EnemyController
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("Move", 1, 1);
    }

    void Move()
    {
        rb.gravityScale *= -1;
    }
}
