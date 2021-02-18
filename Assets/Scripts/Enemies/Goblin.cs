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
    public float sightRadius;
    public float attackRadius;

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
        if (Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer))         // If player is inside attack radius
        {
            rb.velocity = Vector2.zero;
            player.TakeDamage(damage);
        }
        else if (Physics2D.OverlapCircle(transform.position, sightRadius, playerLayer))     // If player is inside sight radius
        {
            rb.velocity = new Vector2(dir * speed, 0);
        }
        else
        {
            rb.velocity = Vector2.zero;
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
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
