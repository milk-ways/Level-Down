using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyController
{
    [Header("Movement")]
    public float speed;
    public float stoppingDis;
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
        int dir = MoveDir();

        if (playerInAtk)         // If player is inside attack radius
        {
            isMoving = false;
            rb.velocity = new Vector2(0, rb.velocity.y);     // Stop moving

            if (attackReady)        // If can attack
            {
                attackReady = false;
                anim.SetTrigger("Attack");
                StartCoroutine(AttackDelay());      // Match attack motion
            }
        }
        else if (playerInSight && attackReady)     // If player is inside sight radius
        {
            isMoving = true;
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);      // Move to player
        }
        else
        {
            //attackReady = true;     // Attack ready if player outside the range
            isMoving = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
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

        if (-stoppingDis < dis && dis < stoppingDis)
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
        yield return new WaitForSeconds(0.2f);
        if (playerInAtk)
            player.TakeDamage(damage);      // Attack
        StartCoroutine(AttackReady());  // Reset attackReady after time
    }

    IEnumerator AttackReady()
    {
        yield return new WaitForSeconds(attackDelay);
        attackReady = true;
    }

    private void OnDrawGizmos()
    {
        // Sight
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + sightRadius, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - sightRadius, transform.position.y, transform.position.z));

        // Attack
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackRadius, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - attackRadius, transform.position.y, transform.position.z));
    }
}
