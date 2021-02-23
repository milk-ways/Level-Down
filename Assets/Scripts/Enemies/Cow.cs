using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : EnemyController
{
    [Header("Movement")]
    public float normalSpeed;               // Normal speed
    public float madSpeed;                  // Mad speed
    float speed;                            // Move speed

    [Header("Attack")]
    public float sightDistance;
    public float returnNormalTime;          // Time for turning back to normal when player out of sight
    float returnNormalTimer;                // Timer
    [SerializeField] bool playerInSight = false;             // True when player in sight
    [SerializeField] bool isMad = false;
    bool hitPlayer = false;

    // Component
    GameObject player;
    SightController sightController;
    Rigidbody2D rb;
    Animator anim;
    CamShake camShake;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sightController = GetComponent<SightController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();            // Cow animator
        camShake = Camera.main.GetComponent<CamShake>();       // Camera animator
    }

    void Update()
    {
        playerInSight = (sightController.PlayerInSight(Vector2.right, sightDistance) || sightController.PlayerInSight(Vector2.left, sightDistance));

        if (playerInSight)
        {
            isMad = true;
        }

        if (isMad && playerInSight && !hitPlayer)        // Dash to player
        {
            int dir = MoveDir();
            anim.SetTrigger("Attack");
            speed = madSpeed;
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);        // Run to player
        }
        else
        {
            speed = normalSpeed;
        }

        if (!playerInSight && isMad && returnNormalTimer <= 0)
        {
            returnNormalTimer = returnNormalTime;
            isMad = false;
        }
        else if (!playerInSight && isMad && returnNormalTimer > 0)
        {
            returnNormalTimer -= Time.deltaTime;
        }

        anim.SetBool("HitPlayer", hitPlayer);
        anim.SetBool("IsMad", isMad);
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hit player
        if(collision.transform.tag == "Player")
        {
            //if (MoveDir() > 0)  // Player on right
            //    camShake.SmallR();
            //else
            //    camShake.SmallR();

            rb.velocity = Vector2.zero;
            hitPlayer = true;
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2);
        hitPlayer = false;
    }
}
