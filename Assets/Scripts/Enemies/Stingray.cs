using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stingray : EnemyController
{
    // Attack related
    [Header("Attack")]
    public float upSpeed;
    public float upOffset;
    [SerializeField] bool isNormal = true;
    [SerializeField] bool isMad = false;
    GameObject player;

    // Components
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        // Go up when normal
        if (isNormal && transform.position.y < player.transform.position.y + upOffset)
        {
            rigidBody.velocity = Vector2.up * upSpeed;
        }
        else
        {
            isNormal = false;
            isMad = true;
        }

        if (isMad)
        {
            anim.SetBool("IsMad", isMad);
            rigidBody.velocity = (player.transform.position - transform.position).normalized * speed;
        }
    }
}
