using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : EnemyController
{
    [Header("Movement")]
    public float stoppingDis;
    public float normalSpeed;               // Normal speed
    public float madSpeed;                  // Mad speed
    float speed;                            // Move speed
    int moveDir;
    Vector2 initPos;

    [Header("Attack")]
    public float sightDistance;
    public float returnNormalTime;          // Time for turning back to normal when player out of sight
    [SerializeField] float returnNormalTimer;                // Timer
    [SerializeField] bool playerInSight = false;             // True when player in sight
    [SerializeField] bool isMad = false;
    bool hitPlayer = false;
    bool isDashing = false;
    public float dashTime;                  // Dash time if not hit player
    [SerializeField] float dashTimer;

    // Component
    GameObject player;
    SightController sightController;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sightController = GetComponent<SightController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();            // Cow animator
        
        initPos = transform.position;
        MoveToLeft();
    }

    void Update()
    {
        playerInSight = (sightController.PlayerInSight(Vector2.right, sightDistance) || sightController.PlayerInSight(Vector2.left, sightDistance));

        if (playerInSight)
        {
            isMad = true;
            returnNormalTimer = returnNormalTime;
        }

        if (isMad)        // Dash to player
        {
            speed = madSpeed;

            if (!isDashing)
            {
                moveDir = MoveToPlayer();
            }

            if (!hitPlayer)
            {
                anim.SetTrigger("Attack");
                isDashing = true;
                rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);        // Run to player
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                    StopDash();
            }
            if (!playerInSight)
            {
                // Player outside of sight
                if (returnNormalTimer <= 0)
                {
                    returnNormalTimer = returnNormalTime;
                    isMad = false;
                    StopDash();
                    initPos = transform.position;
                    MoveToLeft();
                }
                else
                {
                    returnNormalTimer -= Time.deltaTime;
                }
            }
        }
        else
        {
            speed = normalSpeed;

            rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);

            if (transform.position.x - initPos.x >= 3 && moveDir == 1)
                StopMoving();
            if (transform.position.x - initPos.x <= -3 && moveDir == -1)
                StopMoving();
        }

        anim.SetBool("IsDashing", isDashing);
        anim.SetBool("IsMad", isMad);
    }

    void StopDash()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        hitPlayer = true;
        isDashing = false;
        dashTimer = dashTime;
        StartCoroutine(AttackDelay());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Hit player
        if(collision.transform.tag == "Player")
        {
            Invoke("StopDash", 0.05f);
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1);
        hitPlayer = false;
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

    int MoveToPlayer()
    {
        float dis = player.transform.position.x - transform.position.x;

        if (-stoppingDis < dis && dis < stoppingDis)
            return 0;

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return 1;
        }
        else //(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return -1;
        }
    }

    private void OnDrawGizmos()
    {
        // Sight
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + sightDistance, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - sightDistance, transform.position.y, transform.position.z));
    }
}
