using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : EnemyController
{
    [Header("Movement")]
    public float normalSpeed;               // Normal speed
    public float madSpeed;                  // Mad speed
    float speed;                            // Move speed

    [Header("Attack")]
    public LayerMask playerLayer;
    public Transform sightPos;
    public float sightRadius;
    public float returnNormalTime;          // Time for turning back to normal when player out of sight
    float returnNormalTimer;                // Timer
    bool playerInSight = false;             // True when player in sight
    bool isMad = false;

    // Component
    GameObject player;
    Rigidbody2D rb;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerInSight = Physics2D.OverlapCircle(sightPos.position, sightRadius, playerLayer);
        if (playerInSight)
            isMad = true;

        if (!playerInSight && isMad && returnNormalTimer <= 0)
        {
            returnNormalTimer = returnNormalTime;
            isMad = false;
        }
        else if (!playerInSight && isMad && returnNormalTimer > 0)
        {
            returnNormalTimer -= Time.deltaTime;
        }

        if (isMad)
        {
            speed = madSpeed;
            rb.velocity = new Vector2(MoveDir() * speed, rb.velocity.y);
        }
        else
        {
            speed = normalSpeed;
        }
    }

    int MoveDir()
    {
        if (player.transform.position.x > transform.position.x)
            return 1;
        else
            return -1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sightPos.position, sightRadius);      // Melee attack gizmo
    }
}
