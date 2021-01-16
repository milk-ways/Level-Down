using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player basic settings
    [Header("Basic Settings")]
    public int hp;
    
    // Player movement related
    [Header("Movement")]
    public float speed;                             // Move speed
    float moveInput;                                // Horizontal input
    [SerializeField] int faceDir = 1;            // Player facing direction 1:right, -1:left

    // Player dash related
    [Header("Dash")]
    public float dashSpeed;                           // dash speed
    public float defaultDashTime;                  // dash lasting time
    [SerializeField] float dashTime;                 // dash lasting countdown timer
    [SerializeField] bool dash = false;             // true:dash, false:stop dash

    // Player Jump related
    [Header("Jump")]
    public float jumpForce;                      // Jumping force
    float verticalInput;                             // Vertical input
    [SerializeField] int extraJumps = 1;     // Number of jumps

    // Ground Check related
    [Header("Ground Check")]
    public Transform groundCheck;                  // Ground check position
    public LayerMask groundLayer;                  // Ground layer
    public float groundRadius;                         // Ground check radius
    [SerializeField] bool isGrounded;                // True if player on ground

    // Attack Related
    [Header("Attack")]
    public Transform meleeAtkPos;               // Melee attack position
    public LayerMask enemyLayer;                // Enemy layer
    public float meleeAttackRange;                         // Melee attack range (radius)
    public int attackDamage;                        // Attack damage

    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        dashTime = defaultDashTime;     // Reset dash time
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Movement
        moveInput = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);  // Move

        // Player facing direction
        if (faceDir == -1 && moveInput > 0)
            Flip();
        else if (faceDir == 1 && moveInput < 0)
            Flip();

        // Dash
        if (dash)
        {
            if (dashTime <= 0)
            {
                dash = false;
                dashTime = defaultDashTime;
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(faceDir * dashSpeed, rigidBody.velocity.y);
                dashTime -= Time.deltaTime;
            }
        }
    }

    void Update()
    {
        // Jumps
        if (isGrounded)
            extraJumps = 1;     // Reset extraJump if on ground

        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (verticalInput > 0 && extraJumps > 0)  // Up arrow and can jump
            {
                rigidBody.velocity = Vector2.up * jumpForce;    // Jump
                extraJumps--;
            }
            else if (verticalInput < 0 && isGrounded)     // Down arrow and is grounded
            {
                Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer).gameObject.GetComponent<PlatformEffector2D>().surfaceArc = 180f;    // move down
            }
        }

        // Dash
        if (!dash && Input.GetKeyDown(KeyCode.D))
        {
            dash = true;
        }
      

        // Melee Attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(meleeAtkPos.position, meleeAttackRange, enemyLayer);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
        }
    }

    // Set player facing direction
    void Flip()
    {
        faceDir *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);      // Ground check gizmo
        Gizmos.DrawWireSphere(meleeAtkPos.position, meleeAttackRange);      // Melee attack gizmo
    }
}
