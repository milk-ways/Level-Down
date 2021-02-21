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
    bool isHiding = false;

    // Componenets
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bulletAttack = Physics2D.OverlapCircle(transform.position, bulletCheckRadius, bulletLayer);     // Player attacking with bullet

        if (bulletAttack && !isHiding)  // Attacking with bullet but not yet hiding
        {
            Hide();
        }

        if (!bulletAttack && isHiding)  // Not being attacked but is hiding
        {
            Show();
        }
        
    }

    void Hide()
    {
        anim.SetTrigger("Hide");
        isHiding = true;
        getDamage = false;
    }

    void Show()
    {
        anim.SetTrigger("Show");
        isHiding = false;
        getDamage = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bulletCheckRadius);      // Melee attack gizmo
    }
}
