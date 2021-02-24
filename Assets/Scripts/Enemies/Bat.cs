using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController
{
    public float speed;

    GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        speed = 0;
        Invoke("Move", 0.5f);
    }

    void FixedUpdate()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }

    void Move()
    {
        speed = 6;
    }
}