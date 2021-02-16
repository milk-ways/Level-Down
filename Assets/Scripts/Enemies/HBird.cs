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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = new Vector2(0, -downSpeed);
        yield return new WaitForSeconds(downTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(attackWaitTime);
        rb.velocity = new Vector2(attackSpeed, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);      // Melee attack gizmo
    }
}
