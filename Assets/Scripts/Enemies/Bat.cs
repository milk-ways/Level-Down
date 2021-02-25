using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController
{
    public float speed;

    public LayerMask playerLayer;
    public float sight;
    [SerializeField] bool playerWasFound = false;
    public float moveDelay;
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
            playerWasFound = Physics2D.OverlapCircle(transform.position, sight, playerLayer);
            if (playerWasFound)
                InvokeMove();
        }
    }

    void FixedUpdate()
    {
        if (!isMoving)
            return;
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }

    void InvokeMove()
    {
        Invoke("Move", moveDelay);
    }

    void Move()
    {
        isMoving = true;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, sight);
    //}
}