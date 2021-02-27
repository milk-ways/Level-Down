using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    public LayerMask playerLayer;
    public float activateRadius;

    EnemyController enemy;

    void Start()
    {
        enemy = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, activateRadius, playerLayer))
        {
            enemy.enabled = true;
            Destroy(this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activateRadius);
    }
}
