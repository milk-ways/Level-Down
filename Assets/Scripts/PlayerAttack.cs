using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attack Related
    [Header("Melee Attack")]
    public Transform meleeAtkPos;               // Melee attack position
    public LayerMask enemyLayer;                // Enemy layer
    public float meleeAtkRange;                  // Melee attack range (radius)
    public int attackDamage;                        // Attack damage
    [SerializeField] int attackType = 0;            // 0 - hand, 1 - melee, 2 - ranged

    [Header("Ranged Attack")]
    public Transform rangeAtkPos;               // Range attack position
    public GameObject gun;                      // Gun; enable when attack
    public GameObject bullet;                   // Bullet prefab
    public float reloadTime;                    // Time for reloading
    public int maxBulletNum;                            // Max number of bullets
    [SerializeField] int bulletNum;                       // Number of bullets
    [SerializeField] float reloadTimer;              // Bullet reload timer

    [Header("Skill")]
    public GameObject skillPref;                // Skill prefab
    public float skillCoolTime;                 // Skill use cooldown
    [SerializeField] float skillCoolTimer;      // Skill cooldown timer
    [SerializeField] bool skillReady = false;           // true - can use skill

    void Start()
    {
        reloadTimer = reloadTime; // Reset reload time
        bulletNum = maxBulletNum;       // Reset bullet num
        skillCoolTimer = skillCoolTime; // Reset skill cooldown
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
            if (attackType == 0)        // Hand
                Punch();
            else if (attackType == 1)   // Melee
                MeleeAtk();
            else                        // Ranged
                RangedAtk();
        }

        // Reload bullet
        if (bulletNum < maxBulletNum)       // If bullet is used
        {
            if (reloadTime <= 0)
            {
                bulletNum = maxBulletNum;       // Reload
                reloadTimer = reloadTime; // Reset timer
            }
            else
            {
                reloadTimer -= Time.deltaTime;
            }
        }

        // Skill
        if (Input.GetKeyDown(KeyCode.R))
        {
            SkillCast();
        }
        if (!skillReady)
        {
            if (skillCoolTimer <= 0)
            {
                skillReady = true;                  // Set to skill ready
                skillCoolTimer = skillCoolTime;     // Reset timer
            }
            else
                skillCoolTimer -= Time.deltaTime;
        }
    }

    // Hand attack
    void Punch()
    {
        Collider2D enemy = Physics2D.OverlapCircle(meleeAtkPos.position, meleeAtkRange, enemyLayer);
        if (enemy != null)
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
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

    void SkillCast()
    {
        if (skillReady)
        {
            Instantiate(skillPref, transform.position, transform.rotation);
            skillReady = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAtkPos.position, meleeAtkRange);      // Melee attack gizmo
    }
}
