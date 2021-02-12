using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : EnemyController
{
    [Header("Movement")]
    public float speed;
    public int moveSec = 0;
    public int moveDir = 0;

    [Header("Damage")]
    public Transform behindCheck;
    public LayerMask playerLayer;
    public float behindRadius;
    public bool playerIsBehind;

    // Component
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Move();
    }

    void Move()
    {
        moveSec = Random.Range(1, 4);

        int newMoveDir = Random.Range(-1, 2);

        while (moveDir == newMoveDir)
        {
            newMoveDir = Random.Range(-1, 2);
        }

        moveDir = newMoveDir;

        if (moveDir == 1)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (moveDir == -1)
            transform.eulerAngles = new Vector3(0, 0, 0);

        Invoke("Move", moveSec);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
    }

    void Update()
    {
        // Check if player is behind
        playerIsBehind = Physics2D.OverlapCircle(behindCheck.position, behindRadius, playerLayer);
    }

    public override void TakeDamage(int damage)
    {
        // Only get damage if attacked from behind
        if (playerIsBehind)
            base.TakeDamage(damage);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(behindCheck.position, behindRadius);      // Behind check gizmo
    }
}