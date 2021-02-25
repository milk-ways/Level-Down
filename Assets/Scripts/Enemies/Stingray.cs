using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stingray : EnemyController
{
    // Attack related
    [Header("Attack")]
    public float speed;
    public float upSpeed;
    public float upOffset;
    [SerializeField] bool isNormal = true;
    [SerializeField] bool isMad = false;

    public LayerMask playerLayer;
    public float sight;
    [SerializeField] bool isMoving = false;

    // Components
    GameObject player;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving)
        {
            isMoving = Physics2D.OverlapCircle(transform.position, sight, playerLayer);
        }
    }

    void FixedUpdate()
    {
        if (!isMoving)
            return;
        // Go up when normal
        if (isNormal && transform.position.y < player.transform.position.y + upOffset)
        {
            rb.velocity = Vector2.up * upSpeed;
        }
        else
        {
            isNormal = false;
            isMad = true;
        }

        if (isMad)
        {
            anim.SetBool("IsMad", isMad);
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
