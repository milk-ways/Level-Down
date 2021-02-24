using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyController
{
    [Header("Setting")]
    SightController sight;
    public float sightDistance;
    public float speed;
    public float jumpForce;
    public LayerMask layer;

    [Header("State")]
    public bool isMad;
    public bool movingToPlayer;
    public bool canJump;
    public bool isJumping;
    
    Rigidbody2D slimeRigid;
    GameObject player;
    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sight = GetComponent<SightController>();
        slimeRigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (sight.PlayerInSight(Vector2.right, sightDistance) || sight.PlayerInSight(-Vector2.right, sightDistance))
            isMad = true;

        //isMad = (sight.PlayerInSight(Vector2.right, sightDistance) || sight.PlayerInSight(-Vector2.right, sightDistance));
        int dir = MoveDir();

        if (hp == 2 || hp == 1)
        {
            isMad = true;
        }

        if (canJump)
        {
            StartCoroutine(Jump());
        }

        if (isMad)
        {
            if (isJumping)
                slimeRigid.velocity = new Vector2(dir * speed, slimeRigid.velocity.y);        // Move towards player
            else
                slimeRigid.velocity = new Vector2(0, slimeRigid.velocity.y);
        }

        anim.SetBool("CanJump", canJump);
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
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return -1;
        }
    }

    IEnumerator Jump()
    {
        canJump = false;
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(0.3f);
        isJumping = true;
        if (isMad)
            movingToPlayer = true;
        slimeRigid.velocity = Vector2.up * jumpForce;
    }

    private void OnDrawGizmos()
    {
        // Sight
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + sightDistance, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - sightDistance, transform.position.y, transform.position.z));
    }
}