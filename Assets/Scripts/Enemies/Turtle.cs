using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : EnemyController
{
    [Header("Movement")]
    public float speed;
    Vector2 initPos;
    int moveDir;
    bool isMoving = false;

    [Header("Damage")]
    public LayerMask bulletLayer;
    public float bulletCheckRadius;
    [SerializeField] bool bulletAttack = false;         // True if bullet is inside the check radius
    bool isHiding = false;

    // Componenets
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        initPos = transform.position;
        MoveToLeft();
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

        anim.SetBool("IsWalking", isMoving);
    }

    void FixedUpdate()
    {
        if (isHiding)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isMoving = false;
        }
        else
        {
            rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
            isMoving = true;

            if (transform.position.x - initPos.x >= 3 && moveDir == 1)
                StopMoving();
            if (transform.position.x - initPos.x <= -3 && moveDir == -1)
                StopMoving();
        }
    }

    public void StopMoving()
    {
        isMoving = false;
        if (moveDir == 1)
        {
            moveDir = 0;
            Invoke("MoveToLeft", 2);
        }

        if (moveDir == -1)
        {
            moveDir = 0;
            Invoke("MoveToRight", 2);
        }
    }

    void MoveToLeft()
    {
        moveDir = -1;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    void MoveToRight()
    {
        moveDir = 1;
        transform.eulerAngles = new Vector3(0, 0, 0);
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
