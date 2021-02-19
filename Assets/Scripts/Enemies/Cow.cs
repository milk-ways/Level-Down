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
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sightController = GetComponent<SightController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerInSight = (sightController.PlayerInSight(Vector2.right, sightDistance) || sightController.PlayerInSight(Vector2.left, sightDistance));

        if (playerInSight)
            isMad = true;

        if (!playerInSight && isMad && returnNormalTimer <= 0)
        {
            returnNormalTimer = returnNormalTime;
            isMad = false;
        }
        else if (!playerInSight && isMad && returnNormalTimer > 0)
        {
            returnNormalTimer -= Time.deltaTime;
        }

        if (isMad && !hitPlayer)
        {
            speed = madSpeed;
            rb.velocity = new Vector2(MoveDir() * speed, rb.velocity.y);
        }
        else
        {
            speed = normalSpeed;
        }
    }

    int MoveDir()
    {
        if (player.transform.position.x > transform.position.x)
            return 1;
        else
            return -1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
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
