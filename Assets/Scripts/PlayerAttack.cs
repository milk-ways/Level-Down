using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    // Abilities
    public bool meleeEnabled;
    public bool rangeEnabled;
    public bool skillEnabled;
    public bool attackAble = true;

    // Attack Related
    [Header("Melee Attack")]
    public Transform meleeAtkPos;               // Melee attack position
    public LayerMask enemyLayer;                // Enemy layer
    public float meleeAtkRange;                  // Melee attack range (radius)
    public int attackDamage;                        // Attack damage
    public enum AtkType { Hand, Melee, Range };
    public AtkType currentAtkType;              // Current attack type
    List<AtkType> atkTypeList;                  // Available attack types
    int atkTypeIndex = 0;            // 0 - hand, 1 - melee, 2 - ranged

    [Header("Ranged Attack")]
    public Transform rangeAtkPos;               // Range attack position
    public GameObject bullet;                   // Bullet prefab
    public float reloadTime;                    // Time for reloading
    public float rangeAtkDelay;                      // Attack delay for matching animation
    public int maxBulletNum;                            // Max number of bullets
    [SerializeField] int bulletNum;                       // Number of bullets
    [SerializeField] float reloadTimer;              // Bullet reload timer

    [Header("Skill")]
    public GameObject skillPref;                // Skill prefab
    public float skillCoolTime;                 // Skill use cooldown
    [SerializeField] float skillCoolTimer;      // Skill cooldown timer
    [SerializeField] bool skillReady = false;           // true - can use skill

    // Components
    UIController uiController;
    Animator anim;

    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        anim = GetComponent<Animator>();

        reloadTimer = reloadTime; // Reset reload time
        bulletNum = maxBulletNum;       // Reset bullet num
        skillCoolTimer = skillCoolTime; // Reset skill cooldown

        atkTypeList = System.Enum.GetValues(typeof(AtkType)).Cast<AtkType>().ToList();

        // Testing disabled attacks (temp)
        if (!meleeEnabled)
            atkTypeList.Remove(AtkType.Melee);
        if (!rangeEnabled)
            atkTypeList.Remove(AtkType.Range);
    }

    void Update()
    {
        // Swap weapon
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentAtkType = NextAvailableAtk();
            uiController.ChangeWeaponImg(currentAtkType);       // Change weapon UI
        }

        // Attack
        if (attackAble && Input.GetKeyDown(KeyCode.F))
        {
            if (currentAtkType == AtkType.Hand)        // Hand
                Punch();
            else if (currentAtkType == AtkType.Melee)   // Melee
                MeleeAtk();
            else                        // Ranged
                RangedAtk();
        }

        // Reload bullet
        if (bulletNum < maxBulletNum)       // If bullet is used
        {
            if (reloadTimer <= 0)
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
        if (Input.GetKeyDown(KeyCode.R) && skillEnabled)
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

    // Next available attack type
    AtkType NextAvailableAtk()
    {
        atkTypeIndex++;
        if (atkTypeIndex >= atkTypeList.Count)
            atkTypeIndex = 0;

        return atkTypeList[atkTypeIndex];
    }

    // Hand attack
    void Punch()
    {
        // Punch animation
        int rand = Random.Range(0, 2);
        if (rand == 0)
            anim.SetTrigger("Punch1");
        else
            anim.SetTrigger("Punch2");

        Collider2D enemy = Physics2D.OverlapCircle(meleeAtkPos.position, meleeAtkRange, enemyLayer);
        if (enemy != null)
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
    }

    // Melee attack
    void MeleeAtk()
    {
        // Melee atk animation
        anim.SetTrigger("MeleeAtk");

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
            // Range atk animation
            anim.SetTrigger("RangeAtk");

            StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(rangeAtkDelay);
        Instantiate(bullet, rangeAtkPos.position, rangeAtkPos.rotation);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAtkPos.position, meleeAtkRange);      // Melee attack gizmo
    }
}
