using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController
{
    public float speed;

    public LayerMask playerLayer;
    public float stoppingDis;
    public float sight;
    public float moveDelay;
    [SerializeField] bool playerWasFound = false;
    [SerializeField] bool isMoving = false;

    GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!playerWasFound)
        {
            if (Physics2D.OverlapCircle(transform.position, sight, playerLayer))
            {
                playerWasFound = true;
                InvokeMove();
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            int dir = MoveDir();
            if (dir != 0)
                rb.velocity = (player.transform.position - transform.position).normalized * speed;
        }
    }

    void InvokeMove()
    {
        Invoke("Move", moveDelay);
    }

    void Move()
    {
        isMoving = true;
    }

    int MoveDir()
    {
        float dis = Vector2.Distance(player.transform.position, transform.position);

        if (-stoppingDis < dis && dis < stoppingDis)
            return 0;
        return 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}