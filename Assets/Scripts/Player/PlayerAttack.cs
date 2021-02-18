using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Abilities
    public bool meleeEnabled;
    public bool rangeEnabled;
    public bool skillEnabled;
    public bool attackAble = true;

    // Attack Related
    [Header("Attack")]
    int atkTypeIndex = 0;                       // 0 - hand, 1 - melee, 2 - ranged
    public enum AtkType { Hand, Melee, Range };
    public AtkType currentAtkType;              // Current attack type
    List<AtkType> atkTypeList;                  // Available attack types

    [Header("Punch")]
    public float punchAtkTime;                  // Punch attack delay time
    float punchAtkTimer;                        // Punch attack delay timer

    [Header("Melee Attack")]
    public Transform meleeAtkPos;               // Melee attack position
    public LayerMask enemyLayer;                // Enemy layer
    public float meleeAtkRange;                  // Melee attack range (radius)
    public float meleeAtkTime;                 // Melee attack delay time
    public float meleeAtkDelay;
    public int attackDamage;                        // Attack damage
    float meleeAtkTimer;                        // Melee attack delay timer

    [Header("Ranged Attack")]
    public Transform rangeAtkPos;               // Range attack position
    public GameObject bullet;                   // Bullet prefab
    public float reloadTime;                    // Time for reloading
    public float rangeAtkDelay;                      // Attack delay for matching animation
    public int maxBulletNum;                            // Max number of bullets
    [SerializeField] int bulletNum;                       // Number of bullets
    [SerializeField] float reloadTimer;              // Bullet reload timer

    [Header("Skill")]
    public Transform skillAtkPos;               // Skill attack position (temp melee atk pos)
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

        // Initialize attacks
        atkTypeList = new List<AtkType>();
        atkTypeList.Add(AtkType.Melee);
        atkTypeList.Add(AtkType.Range);
        currentAtkType = atkTypeList[0];
        uiController.ChangeWeaponImg(currentAtkType);
    }

    void Update()
    {
        anim.SetFloat("State", CurrentState());

        // Swap weapon
        if (InputManager.instance.KeyDown("Swap"))       // Input.GetKeyDown(KeyCode.S)
        {
            currentAtkType = NextAvailableAtk();
            uiController.ChangeWeaponImg(currentAtkType);       // Change weapon UI
        }

        // Attack
        if (attackAble && InputManager.instance.KeyDown("Fire1"))         // Input.GetKeyDown(KeyCode.F)
        {
            if (currentAtkType == AtkType.Hand && punchAtkTimer <= 0)        // Hand
            {
                Punch();
                punchAtkTimer = punchAtkTime;
            }
            else if (currentAtkType == AtkType.Melee && meleeAtkTimer <= 0)   // Melee
            {
                MeleeAtk();
                meleeAtkTimer = meleeAtkTime;       // Reset timer
            }
            else if (currentAtkType == AtkType.Range)                       // Ranged
                RangedAtk();
        }

        // Attack delay timer
        if (punchAtkTimer > 0)
            punchAtkTimer -= Time.deltaTime;
        if (meleeAtkTimer > 0)
            meleeAtkTimer -= Time.deltaTime;


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
        if (attackAble && skillEnabled && InputManager.instance.KeyDown("Fire2"))       // Input.GetKeyDown(KeyCode.R)
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

    public float CurrentState()
    {
        switch (currentAtkType)
        {
            case AtkType.Hand:
                return 1;

            case AtkType.Melee:
                return 2;

            case AtkType.Range:
                return 3;

            default:
                return 1;
        }
    }

    // Remove melee attack
    public void RemoveMelee()
    {
        meleeEnabled = false;
        atkTypeList.Insert(0, AtkType.Hand);
        atkTypeList.Remove(AtkType.Melee);

        if (currentAtkType == AtkType.Melee)
            currentAtkType = AtkType.Hand;
        uiController.ChangeWeaponImg(currentAtkType);       // Change weapon UI
    }

    // Remove range attack
    public void RemoveRange()
    {
        rangeEnabled = false;
        atkTypeList.Remove(AtkType.Range);
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
        anim.SetFloat("PunchState", (float)rand);
        anim.SetTrigger("Punch");

        Collider2D enemy = Physics2D.OverlapCircle(meleeAtkPos.position, meleeAtkRange, enemyLayer);
        if (enemy != null)
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
    }

    // Melee attack
    void MeleeAtk()
    {
        // Melee atk animation
        anim.SetTrigger("MeleeAtk");

        StartCoroutine(MeleeAttackDelay());
    }

    // Ranged attack
    void RangedAtk()
    {
        if (bulletNum > 0)
        {
            // Range atk direction
            anim.SetFloat("RangeAtkState", InputManager.instance.VerticalAxis());
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
            Instantiate(skillPref, skillAtkPos.position, skillAtkPos.rotation);
            skillReady = false;
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(rangeAtkDelay);
        Instantiate(bullet, rangeAtkPos.position, rangeAtkPos.rotation);
    }

    IEnumerator MeleeAttackDelay()
    {
        yield return new WaitForSeconds(meleeAtkDelay);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(meleeAtkPos.position, meleeAtkRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAtkPos.position, meleeAtkRange);      // Melee attack gizmo
    }
}
