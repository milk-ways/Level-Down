using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player basic settings
    [Header("Basic Settings")]
    public int hp;
    bool immortal = false;      // Can't be damaged when immortal
    [SerializeField] bool dashEnabled = true;       // true:can dash
    [SerializeField] bool jumpEnabled = true;    // true:multiple jump, false:1 jump

    // Player movement related
    [Header("Movement")]
    public float speed;                             // Move speed
    float moveInput;                                // Horizontal input
    float verticalInput;                            // Vertical input
    [SerializeField] int faceDir = 1;            // Player facing direction 1:right, -1:left
    bool isWalking = false;

    // Player dash related
    [Header("Dash")]
    public float dashSpeed;                           // dash speed
    public float defaultDashTime;                  // dash lasting time
    [SerializeField] float dashTime;                 // dash lasting countdown timer
    [SerializeField] bool dash = false;             // true:dash, false:stop dash

    // Player Jump related
    [Header("Jump")]
    public float jumpForce;                      // Jumping force
    public int maxJumps;                        // Max number of jumps
    [SerializeField] int extraJumps = 1;        // Number of jumps
    bool isJumping = false;

    // Ground Check related
    [Header("Ground Check")]
    public Transform groundCheck;                  // Ground check position
    public LayerMask groundLayer;                  // Ground layer
    public float groundRadius;                         // Ground check radius
    [SerializeField] bool isGrounded;                // True if player on ground
    [SerializeField] bool wasGrounded;              // Check if player was on ground in prev frame (need for jump check)
        
    // Components
    Rigidbody2D rigidBody;
    Animator anim;
    PlayerAttack playerAtk;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAtk = GetComponent<PlayerAttack>();

        dashTime = defaultDashTime;     // Reset dash time
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Movement
        moveInput = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);  // Move
        if (moveInput != 0)
            isWalking = true;
        else
            isWalking = false;

        // Player facing direction
        if (faceDir == -1 && moveInput > 0)
            Flip();
        else if (faceDir == 1 && moveInput < 0)
            Flip();

        // Move animation
        anim.SetBool("IsWalking", isWalking);

        // Dash
        if (dash)
        {
            if (dashTime <= 0)
            {
                dash = false;
                immortal = false;
                playerAtk.attackAble = true;
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
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (verticalInput < 0 && isGrounded)     // Down arrow and is grounded
            {
                Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer).gameObject.GetComponent<PlatformEffector2D>().surfaceArc = 180f;    // move down
            }
            else if (isGrounded)  // First jump (when on ground)
            {
                rigidBody.velocity = Vector2.up * jumpForce;    // Jump
                isJumping = true;
            }
            else if (!isGrounded && jumpEnabled && extraJumps > 0) // Extra jumps (when not on ground)
            {
                extraJumps--;
                rigidBody.velocity = Vector2.up * jumpForce;    // Jump
                isJumping = true;
                playerAtk.attackAble = false;
            }
        }

        if (isJumping && !isGrounded)
            wasGrounded = false;

        // Jump animation
        anim.SetBool("IsJumping", isJumping);

        // End jump
        if (!wasGrounded && isGrounded)
        {
            extraJumps = maxJumps;     // Reset extraJump if on ground
            isJumping = false;
            playerAtk.attackAble = true;
            wasGrounded = true;
        }

        // Dash
        if (!dash && dashEnabled && Input.GetKeyDown(KeyCode.D))
        {
            dash = true;
            immortal = true;                    // Don't recieve damage when dashing
            playerAtk.attackAble = false;       // Can't attack when dashing
        }

        // Dash animation
        anim.SetBool("IsDashing", dash);
    }

    public void TakeDamage(int damage)
    {
        if (!immortal)
        {
            hp -= damage;
            Debug.Log("Player damage taken");
        }
    }


    // Set player facing direction
    void Flip()
    {
        faceDir *= -1;

        transform.Rotate(0f, 180f, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);      // Ground check gizmo
    }
}
