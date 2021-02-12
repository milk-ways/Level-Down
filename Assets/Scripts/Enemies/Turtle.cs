using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : EnemyController
{
    // Movement
    public float speed;

    // Damage
    public LayerMask bulletLayer;
    public float bulletCheckRadius;
    [SerializeField] bool bulletAttack = false;         // True if bullet is inside the check radius

    void Update()
    {
        bulletAttack = Physics2D.OverlapCircle(transform.position, bulletCheckRadius, bulletLayer);
        getDamage = !bulletAttack;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bulletCheckRadius);      // Melee attack gizmo
    }
}
