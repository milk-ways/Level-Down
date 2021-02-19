using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyController
{
    [Header("Movement")]
    public float speed;
    public int dir = 0;

    [Header("Attack")]
    public float sightRadius;
    public float attackRadius;
    bool playerInSight = false;
    bool playerInAtk = false;

    // Components
    PlayerController player;
    SightController sightController;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sightController = GetComponent<SightController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (playerInAtk)         // If player is inside attack radius
        {
            rb.velocity = Vector2.zero;
            player.TakeDamage(damage);
        }
        else if (playerInSight)     // If player is inside sight radius
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
        playerInSight = sightController.PlayerInSight(Vector2.right, sightRadius) || sightController.PlayerInSight(Vector2.left, sightRadius);
        playerInAtk = sightController.PlayerInSight(Vector2.right, attackRadius) || sightController.PlayerInSight(Vector2.left, attackRadius);

        if (player.transform.position.x > transform.position.x)
            dir = 1;
        else if (player.transform.position.x < transform.position.x)
            dir = -1;
        else
            dir = 0;
    }
}
