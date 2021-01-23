using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player basic settings
    [Header("Basic Settings")]
    public int hp;
    bool immortal = false;      // Can't be damaged when immortal
    
    // Player movement related
    [Header("Movement")]
    public float speed;                             // Move speed
    float moveInput;                                // Horizontal input
    float verticalInput;                            // Vertical input
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
    [SerializeField] int extraJumps = 1;     // Number of jumps

    // Ground Check related
    [Header("Ground Check")]
    public Transform groundCheck;                  // Ground check position
    public LayerMask groundLayer;                  // Ground layer
    public float groundRadius;                         // Ground check radius
    [SerializeField] bool isGrounded;                // True if player on ground

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
                immortal = false;
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
        if (isGrounded)
            extraJumps = 1;     // Reset extraJump if on ground

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (verticalInput < 0 && isGrounded)     // Down arrow and is grounded
            {
                Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer).gameObject.GetComponent<PlatformEffector2D>().surfaceArc = 180f;    // move down
                extraJumps--;
            }
            else if (extraJumps > 0)  // Has more than 1 jump counts
            {
                rigidBody.velocity = Vector2.up * jumpForce;    // Jump
                extraJumps--;
            }
        }

        // Dash
        if (!dash && Input.GetKeyDown(KeyCode.D))
        {
            dash = true;
            immortal = true;
        }
      
        
    }

    public void TakeDamage(int damage)
    {
        if (!immortal)
            hp -= damage;
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
