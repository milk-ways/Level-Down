using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attack Related
    [Header("Attack")]
    // Melee
    public Transform meleeAtkPos;               // Melee attack position
    public LayerMask enemyLayer;                // Enemy layer
    public float meleeAtkRange;                  // Melee attack range (radius)
    public int attackDamage;                        // Attack damage
    [SerializeField] int attackType = 0;            // 0 - hand, 1 - melee, 2 - ranged
    // Ranged
    public Transform rangeAtkPos;               // Range attack position
    public GameObject gun;                      // Gun; enable when attack
    public GameObject bullet;                   // Bullet prefab
    public float defaultReloadTime;                    // Time for reloading
    public int maxBulletNum;                            // Max number of bullets
    [SerializeField] int bulletNum;                       // Number of bullets
    [SerializeField] float reloadTime;              // Bullet reload timer

    void Start()
    {
        reloadTime = defaultReloadTime; // Reset reload time
        bulletNum = maxBulletNum;       // Reset bullet num
    }

    void Update()
    {
        // Swap weapon
        if (Input.GetKeyDown(KeyCode.S))
        {
            attackType++;
            if (attackType > 2)
                attackType = 0;
        }
        if (attackType == 2)
            gun.SetActive(true);
        else
            gun.SetActive(false);

        // Attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (attackType < 2)     // Hand, Melee
                MeleeAtk();
            else
                RangedAtk();
        }

        // Reload bullet
        if (bulletNum < maxBulletNum)       // If bullet is used
        {
            if (reloadTime <= 0)
            {
                bulletNum = maxBulletNum;       // Reload
                reloadTime = defaultReloadTime; // Reset timer
            }
            else
            {
                reloadTime -= Time.deltaTime;
            }
        }
    }

    // Melee attack
    void MeleeAtk()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(meleeAtkPos.position, meleeAtkRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }

    // Ranged attack
    void RangedAtk()
    {
        if (bulletNum > 0)
        {
            Instantiate(bullet, rangeAtkPos.position, rangeAtkPos.rotation);
            bulletNum--;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAtkPos.position, meleeAtkRange);      // Melee attack gizmo
    }
}
