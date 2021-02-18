using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : EnemyController
{
    [Header("Setting")]
    public float rayLength;
    public LayerMask playerLayer;
    public float bulletSpeed;
    public float waitTime;
    public GameObject bullet;

    [Header("State")]
    public bool waiting;        //waiting for attack
    Rigidbody2D bulletRigid;
    private void Start()
    {
        bulletRigid = bullet.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(!waiting)                //doesn't sense during waiting
        {
            sense();
        }
    }

    void attackPlayer()
    {
        waiting = false;
        Rigidbody2D bullet_pre = Instantiate(bulletRigid, transform.position, Quaternion.Euler(0, 0, 0));
        bullet_pre.velocity = Vector2.up * bulletSpeed;
        Destroy(bullet_pre.gameObject, 1f);
    }
    void sense()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.up, 5f, playerLayer);
        if (hit.collider != null)
        {
            waiting = true;
            Invoke("attackPlayer",3f);
        }
    }
}
