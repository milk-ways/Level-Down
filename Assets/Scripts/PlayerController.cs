using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement related
    [Header("Movement")]
    public float speed;         // Move speed
    float moveInput;            // Horizontal input
    [SerializeField] int faceDir = 1;            // Player facing direction 1:right, -1:left

    // Player dash related
    [Header("Dash")]
    public float dashSpeed;             // dash speed
    public float defaultDashTime;       // dash time
    [SerializeField] float dashTime;    // dash timer
    [SerializeField] bool dash = false;                  // true:dash, false:stop dash


    // Player Jump related
    [Header("Jump")]
    public float jumpForce; // Jumping force
    float verticalInput;    // Vertical input
    [SerializeField] int extraJumps = 1;     // Number of jumps

    // Ground Check related
    [Header("Ground Check")]
    public Transform groundCheck;   // Ground check position
    public LayerMask groundLayer;   // Ground layer
    public float groundRadius;      // Ground check radius
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

        moveInput = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);  // Move

        // Player facing direction
        if (faceDir == -1 && moveInput > 0)
            Flip();
        else if (faceDir == 1 && moveInput < 0)
            Flip();

        if (!dash)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dash = true;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                dashTime = defaultDashTime;
                rigidBody.velocity = Vector2.zero;
                dash = false;
            }
            else
            {
                dashTime -= Time.deltaTime;
                rigidBody.velocity = new Vector2(faceDir * dashSpeed, rigidBody.velocity.y);
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
            else if (verticalInput < 0 && isGrounded)     // Down arrow
            {
                Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer).gameObject.GetComponent<PlatformEffector2D>().surfaceArc = 180f;    // move down
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

    void Dash()
    {

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
