using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement related
    public float speed;         // Move speed
    float moveInput;            // Horizontal input
    bool facingRight = true;    // Player facing direction

    // Player Jump related
    public float jumpForce; // Jumping force
    float verticalInput;    // Vertical input
    int extraJumps = 1;     // Number of jumps

    // Ground Check related
    public Transform groundCheck;   // Ground check position
    public LayerMask groundLayer;   // Ground layer
    public float groundRadius;      // Ground check radius
    bool isGrounded;                // True if player on ground
    
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        moveInput = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);  // Move

        // Player facing direction
        if (!facingRight && moveInput > 0)
            Flip();
        else if (facingRight && moveInput < 0)
            Flip();
    }

    void Update()
    {
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

    // 플레이어 바라보는 방향 설정
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
