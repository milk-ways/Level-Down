using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : EnemyController
{
    [Header("Movement")]
    public float speed;
    int moveDir;
    Vector2 initPos;

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

        initPos = transform.position;
        MoveToLeft();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);

        if (transform.position.x - initPos.x >= 3 && moveDir == 1)
            StopMoving();
        if (transform.position.x - initPos.x <= -3 && moveDir == -1)
            StopMoving();
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

    public void StopMoving()
    {
        if (moveDir == 1)
        {
            moveDir = 0;
            Invoke("MoveToLeft", 2);
        }
        
        if (moveDir == -1)
        {
            moveDir = 0;
            Invoke("MoveToRight", 2);
        }
    }

    void MoveToLeft()
    {
        moveDir = -1;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    void MoveToRight()
    {
        moveDir = 1;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(behindCheck.position, behindRadius);      // Behind check gizmo
    }
}