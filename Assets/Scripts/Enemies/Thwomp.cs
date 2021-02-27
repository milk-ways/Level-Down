using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : EnemyController
{
    public bool startDown;             // True: start falling, False: start going up

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!startDown)
        {
            rb.gravityScale *= -1;
        }

        InvokeRepeating("Move", 1, 1);
    }

    void Move()
    {
        rb.gravityScale *= -1;
    }
}
