using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyController
{
    [Header("Movement")]
    public float speed;
    bool isMoving = false;

    [Header("Attack")]
    public float sightRadius;
    public float attackRadius;
    public float attackDelay;
    bool playerInSight = false;
    bool playerInAtk = false;
    bool attackReady = true;

    // Components
    PlayerController player;
    SightController sightController;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sightController = GetComponent<SightController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (playerInAtk)         // If player is inside attack radius
        {
            isMoving = false;
            rb.velocity = Vector2.zero;     // Stop moving

            if (attackReady)        // If can attack
            {
                attackReady = false;
                anim.SetTrigger("Attack");
                StartCoroutine(AttackDelay());      // Match attack motion
            }
        }
        else if (playerInSight)     // If player is inside sight radius
        {
            isMoving = true;
            int dir = MoveDir();
            rb.velocity = new Vector2(dir * speed, 0);      // Move to player
        }
        else
        {
            attackReady = true;     // Attack ready if player outside the range
            isMoving = false;
            rb.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        playerInSight = sightController.PlayerInSight(Vector2.right, sightRadius) || sightController.PlayerInSight(Vector2.left, sightRadius);
        playerInAtk = sightController.PlayerInSight(Vector2.right, attackRadius) || sightController.PlayerInSight(Vector2.left, attackRadius);

        anim.SetBool("IsMoving", isMoving);
    }

    int MoveDir()
    {
        float dis = player.transform.position.x - transform.position.x;

        if (-0.05 < dis && dis < 0.05)
            return 0;

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return 1;
        }
        else //(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return -1;
        }
    }

    // Match attack with motion
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.1f);
        player.TakeDamage(damage);      // Attack
        StartCoroutine(AttackReady());  // Reset attackReady after time
    }

    IEnumerator AttackReady()
    {
        yield return new WaitForSeconds(attackDelay);
        attackReady = true;
    }
}
