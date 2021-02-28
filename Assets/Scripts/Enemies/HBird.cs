using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBird : EnemyController
{
    [Header("Movement")]
    public float downSpeed;
    public float downTime;

    [Header("Attack")]
    public LayerMask playerLayer;
    public float attackSpeed;
    public float attackWaitTime;
    public float attackRadius;
    bool playerInSight = false;

    // Components
    Rigidbody2D rb;
    Animator anim;
    PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer) && !playerInSight)    // Player in sight but not yet attacking
        {
            playerInSight = true;
            StartCoroutine(Attack());
            Destroy(gameObject, 20);
        }
    }


    IEnumerator Attack()
    {
        int dir = Dir();
        anim.SetTrigger("Fly");
        rb.velocity = new Vector2(0, -downSpeed);
        yield return new WaitForSeconds(downTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(attackWaitTime);
        anim.SetTrigger("Attack");
        rb.velocity = new Vector2(attackSpeed * dir, 0);

        Destroy(gameObject, 5f);
    }

    int Dir()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return 1;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return -1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);      // Melee attack gizmo
    }
}
