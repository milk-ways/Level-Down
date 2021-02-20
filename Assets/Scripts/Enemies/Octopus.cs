using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : EnemyController
{
    [Header("Attack")]
    public LayerMask playerLayer;
    public GameObject bulletGO;
    public Transform playerCheckPos;
    public Vector2 playerCheckSize;
    bool playerCheck = false;
    bool attacking = false;

    void Update()
    {
        playerCheck = Physics2D.OverlapBox(playerCheckPos.position, playerCheckSize, 0, playerLayer); // Check if player is on platform
        
        if (playerCheck && !attacking)          // Player above the platform and not yet attacking
        {
            Attack();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletGO, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(bullet, 1f);
        attacking = false;
    }

    void Attack()
    {
        attacking = true;
        Invoke("Shoot", 3f);     // Shoot after 3 seconds
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerCheckPos.position, new Vector3(playerCheckSize.x, playerCheckSize.y, 0));      // Ground check gizmo
    }
}
