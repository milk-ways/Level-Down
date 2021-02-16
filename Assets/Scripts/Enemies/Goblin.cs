using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyController
{
    [Header("Movement")]
    public float speed;
    public int dir = 0;

    [Header("Attack")]
    public LayerMask playerLayer;
    public float attackRadius;
    public bool playerInRange = false;

    // Components
    PlayerController player;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            player.TakeDamage(damage);
        }
        else if (playerInRange)
        {
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }
    }

    void Update()
    {
        if (player.transform.position.x > transform.position.x)
            dir = 1;
        else if (player.transform.position.x < transform.position.x)
            dir = -1;
        else
            dir = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);      // Melee attack gizmo
    }
}
