using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [Header("Movement")]
    public float speed;

    [Header("Attacks")]
    public LayerMask playerLayer;
    public Transform attackPos;
    public float attackRadius;
    public float patternTime;           // Delay time between patterns
    bool playerInAtkRange = false;      // Check if player is inside attack range

    public float pattern1DelayTime;

    // Components
    Rigidbody2D rb;
    PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        playerInAtkRange = Physics2D.OverlapCircle(attackPos.position, attackRadius, playerLayer);

        
    }

    void StartPattern()
    {
        int rand = Random.Range(1, 101);
    }

    IEnumerator Pattern1()
    {
        yield return new WaitForSeconds(pattern1DelayTime);
        if (playerInAtkRange)
        {
            player.TakeDamage(1);
        }
    }
}
