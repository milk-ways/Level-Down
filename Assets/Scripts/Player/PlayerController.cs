using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player basic settings
    [Header("Basic Settings")]
    public int hp;
    public bool dashEnabled = true;     // true:can dash
    public bool jumpEnabled = true;     // true:can super jump
    [SerializeField] bool immortal = false;              // Can't be damaged when immortal
    public GameObject banPanel;         // Temp

    // Player movement related
    [Header("Movement")]
    public float speed;                             // Move speed
    float moveInput;                                // Horizontal input
    [SerializeField] int faceDir = 1;               // Player facing direction 1:right, -1:left
    bool isWalking = false;

    // Player dash related
    [Header("Dash")]
    public float dashSpeed;                         // dash speed
    public float defaultDashTime;                   // dash lasting time
    [SerializeField] float dashTime;                // dash lasting countdown timer
    [SerializeField] bool dash = false;             // true:dash, false:stop dash

    // Player Jump related
    [Header("Jump")]
    public float jumpForce;                     // Jumping force
    public int maxJumps;                        // Max number of jumps
    [SerializeField] int extraJumps = 1;        // Number of jumps
    [SerializeField] bool isJumping = false;
    [SerializeField] bool isFalling = false;

    [Header("Super Jump")]
    public float superJumpForce;
    [SerializeField] bool isSuperJumping = false;

    // Ground Check related
    [Header("Ground Check")]
    public Transform groundCheck;                   // Ground check position
    public LayerMask groundLayer;                   // Ground layer
    public Vector2 groundSize;                      // Ground check width x length
    [SerializeField] bool isGrounded;               // True if player on ground
    [SerializeField] bool wasGrounded;              // Check if player was on ground in prev frame (need for jump check)

    [Header("Damage")]
    public GameObject deathParticle;        // Death particle when dead
    public Color damageColor;
    public float damageImmortalTime;    // Become immortal when attacked
    public float alphaTime;                 // Time for flickering when damaged

    // Components
    Rigidbody2D rigidBody;
    SpriteRenderer sprite;
    Animator anim;
    PlayerAttack playerAtk;
    CamShake camShake;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerAtk = GetComponent<PlayerAttack>();
        camShake = Camera.main.GetComponent<CamShake>();

        dashTime = defaultDashTime;     // Reset dash time

        dashEnabled = GameController.instance.dashEnabled;
        jumpEnabled = GameController.instance.jumpEnabled;
    }

    void FixedUpdate()
    {
        // Movement
        moveInput = InputManager.instance.HorizontalAxis();        // Input.GetAxisRaw("Horizontal");
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
        // Ground check
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundSize, 0, groundLayer);

        // Jumps
        if (InputManager.instance.KeyDown("Jump"))           // Input.GetKeyDown(KeyCode.Space)
        {
            if (isGrounded)  // First jump (when on ground)
            {
                rigidBody.velocity = Vector2.up * jumpForce;    // Jump
                isJumping = true;
                anim.SetTrigger("Jump");
            }
            else if (!isGrounded && !isSuperJumping && extraJumps > 0) // Extra jumps (when not on ground)
            {
                extraJumps--;
                rigidBody.velocity = Vector2.up * jumpForce;    // Jump
                isJumping = true;               // Required if jumping while falling from ground
                playerAtk.attackAble = false;
                anim.SetTrigger("DoubleJump");
            }
        }

        // Super jump
        if (InputManager.instance.KeyDown("SuperJump"))
        {
            if (isGrounded && jumpEnabled)
            {
                rigidBody.velocity = Vector2.up * superJumpForce;
                isJumping = true;
                isSuperJumping = true;
                anim.SetTrigger("Jump");
            }
        }

        if (!isGrounded)
        {
            if (!isFalling)
            {
                isJumping = true;
                anim.SetTrigger("Jump");
                isFalling = true;
            }
        }

        if (isJumping && !isGrounded)
            wasGrounded = false;

        // Jump animation
        anim.SetBool("IsJumping", isJumping);

        // Land on ground (End jump)
        if (!wasGrounded && isGrounded)
        {
            extraJumps = maxJumps;     // Reset extraJump if on ground
            isJumping = false;
            isSuperJumping = false;
            playerAtk.attackAble = true;
            wasGrounded = true;
            isFalling = false;
        }

        // Dash
        if (!dash && dashEnabled && InputManager.instance.KeyDown("Dash"))       // Input.GetKeyDown(KeyCode.D)
        {
            dash = true;
            isFalling = false;
            immortal = true;                    // Don't recieve damage when dashing
            playerAtk.attackAble = false;       // Can't attack when dashing
        }

        // Dash animation
        anim.SetBool("IsDashing", dash);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            TakeDamage(collision.GetComponent<EnemyController>().damage);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Temp
        if (collision.tag == "Finish")
        {
            banPanel.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!immortal && damage > 0)
        {
            hp -= damage;

            // HP goes below 0
            if (hp <= 0)
            {
                Die();
            }
            else
            {
                sprite.color = damageColor;
                immortal = true;        // Set immortal after taking damage
                //Debug.Log("Player damage taken");
                StartCoroutine(DamageImmortal());     // Reset immortal after time
                camShake.SmallRand();       // Camera shake
            }
        }
    }

    IEnumerator DamageImmortal()
    {
        yield return new WaitForSeconds(damageImmortalTime);
        sprite.color = Color.white;
        immortal = false;
    }

    // Set player facing direction
    void Flip()
    {
        faceDir *= -1;

        transform.Rotate(0f, 180f, 0f);
    }

    void Die()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        camShake.PlayerDie();
        Debug.Log("Dead");
        gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(groundSize.x, groundSize.y, 0));      // Ground check gizmo
    }
}
