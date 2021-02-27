using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearmoai : EnemyController
{
    [Header("Sight")]
    public float sightDistance;
    public float sightYOffset;
    bool inSight;                       // Player in attack range

    [Header("Attack")]
    public LayerMask playerLayer;
    public Transform attackPos;
    public Vector2 attackArea;
    public float attackDelay;           // Delay before attacking
    bool isAttacking = false;

    Animator anim;
    SightController sight;
    PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sight = GetComponent<SightController>();
        anim = GetComponent<Animator>();

        anim.SetTrigger("Charge");
    }

    void Update()
    {
        inSight = sight.PlayerInSight(Vector2.right, sightDistance, sightYOffset) || sight.PlayerInSight(-Vector2.right, sightDistance, sightYOffset);

        if (inSight && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);

        FaceDir();
        anim.SetTrigger("Attack");
        if (Physics2D.OverlapBox(attackPos.position, attackArea, 0, playerLayer))
        {
            player.TakeDamage(damage);
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    void FaceDir()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else //(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void OnDrawGizmos()
    {
        // Attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackArea.x, attackArea.y, 0));

        // Sight
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - sightYOffset,transform.position.z), new Vector3(transform.position.x + sightDistance, transform.position.y - sightYOffset, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - sightYOffset, transform.position.z), new Vector3(transform.position.x - sightDistance, transform.position.y - sightYOffset, transform.position.z));
    }
}
