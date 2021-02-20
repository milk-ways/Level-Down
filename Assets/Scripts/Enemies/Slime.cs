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
    public bool seePlayer;
    public bool isMad;
    public bool canJump;
    
    Rigidbody2D slimeRigid;
    Collider2D slimeCollider;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sight = GetComponent<SightController>();
        slimeRigid = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponentInChildren<Collider2D>();
    }
    private void Update()
    {
        if(!isMad)
        {
            seePlayer = sight.PlayerInSight(Vector2.right, sightDistance) || sight.PlayerInSight(-Vector2.right, sightDistance);
        }
        if(seePlayer)
        {
            isMad = true;
        }
        if(isMad)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            slimeRigid.velocity = new Vector2(dir.x,0) * speed;
            transform.right = new Vector2(dir.x, 0);
            if (canJump)
            {
                Jump();
            }            
        }
        if(hp == 2 || hp == 1)
        {
            isMad = true;
        }
    }

    void Jump()
    {
        slimeRigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }   
}