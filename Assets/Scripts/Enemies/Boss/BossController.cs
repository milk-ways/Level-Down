using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [Header("Movement")]
    public float speed;
    bool isMoving = false;

    [Header("Attacks")]
    public LayerMask playerLayer;
    public Transform attackPos;
    public float attackRadius;
    public float patternTime;                           // Delay time between patterns
    public float endPatternTime;
    [SerializeField] bool playerInAtkRange = false;     // Check if player is inside attack range
    [SerializeField] bool isUsingPattern = false;       // True when pattern is enabled

    [Header("Pattern 1")]
    public float pattern1DelayTime;
    public int pattern1Damage;                      // Pattern 1 damage

    [Header("Pattern 2")]
    public GameObject pattern2;                     // Pattern 2 prefab
    public Transform pattern2Pos;                   // Position for instantiating pattern 2
    public float pattern2DelayTime;

    [Header("Pattern 3")]
    public GameObject pattern3;                     // Pattern 3 prefab
    public Transform pattern3Pos;
    public float pattern3DelayTime;

    [Header("Pattern 4")]
    public GameObject bullet;                       // Bullet prefab
    public Transform pattern4Pos;                   // Position for shooting bullet
    public float pattern4DelayTime;
    public float punchMotionTime;                   // Time for punch motion

    [Header("Pattern 5")]
    public float pattern5DelayTime; 
    public float jumpForce;                         // Jumping force
    public int repeat;                              // Number of times repeating pattern 5
    float gravity;

    [Header("Pattern 6")]
    public GameObject bat;                          // Bat prefab
    public float pattern6DelayTime;

    // Components
    Rigidbody2D rb;
    PlayerController player;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        gravity = rb.gravityScale;

        StartCoroutine(StartPattern());
    }

    void Update()
    {
        playerInAtkRange = Physics2D.OverlapCircle(attackPos.position, attackRadius, playerLayer);

        if (!isUsingPattern)
        {
            isMoving = true;
            rb.velocity = new Vector2(MoveDir() * speed, rb.velocity.y);        // Move towards player
        }

        if (playerInAtkRange || isUsingPattern)
        {
            isMoving = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        anim.SetBool("IsMoving", isMoving);
    }

    int MoveDir()
    {
        float dis = player.transform.position.x - transform.position.x;

        if (-0.05 < dis && dis < 0.05)
            return 0;

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return 1;
        }
        else //(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return -1;
        }
    }

    IEnumerator StartPattern()
    {
        yield return new WaitForSeconds(patternTime);

        isMoving = false;
        rb.velocity = new Vector2(0, rb.velocity.y);        // Stop moving towards player

        int rand = Random.Range(1, 101);

        if (0 < rand && rand < 101)          // Between 1~20 (20%)
            StartCoroutine(Pattern3());
        else if (20 < rand && rand < 36)    // Between 21~35 (15%)
            StartCoroutine(Pattern2());
        else if (35 < rand && rand < 51)    // Between 36~50 (15%)
            StartCoroutine(Pattern3());
        else if (50 < rand && rand < 71)    // Between 51~70 (20%)
            StartCoroutine(Pattern4());
        else if (70 < rand && rand < 91)    // Between 71~90 (20%)
            StartCoroutine(Pattern5(0));
        else                                // Between 91~100 (10%)
            StartCoroutine(Pattern6());
    }

    IEnumerator Pattern1()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 1 (Slash)");
        anim.SetTrigger("SlashReady");

        yield return new WaitForSeconds(pattern1DelayTime);
        anim.SetTrigger("Slash");
        if (playerInAtkRange)
        {
            player.TakeDamage(pattern1Damage);
        }

        yield return new WaitForSeconds(endPatternTime);
        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern2()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 2 (Bottom Shoot)");

        yield return new WaitForSeconds(pattern2DelayTime);
        Instantiate(pattern2, pattern2Pos.position, Quaternion.identity);

        yield return new WaitForSeconds(endPatternTime);
        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern3()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("pattern 3 (Purple)");

        Instantiate(pattern3, pattern3Pos.position, Quaternion.identity);       // Create purple effect
        yield return new WaitForSeconds(pattern3DelayTime);

        yield return new WaitForSeconds(endPatternTime);
        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern4()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 4 (Punch)");
        anim.SetTrigger("PunchReady");

        yield return new WaitForSeconds(pattern4DelayTime);
        anim.SetTrigger("Punch");
        Instantiate(bullet, pattern4Pos.position, transform.rotation);          // Create punch effect

        yield return new WaitForSeconds(endPatternTime + punchMotionTime);
        anim.SetTrigger("PunchEnd");
        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern5(int index)
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 5 (Jump)");

        anim.SetTrigger("JumpReady");
        yield return new WaitForSeconds(pattern5DelayTime);
        Vector3 playerPos = player.transform.position;          // Current location of player
        rb.gravityScale = 0;
        anim.SetTrigger("Jump");
        rb.velocity = Vector2.up * jumpForce;                   // Move up
        yield return new WaitForSeconds(pattern5DelayTime);     // Going up
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);      // Move to player x position
        rb.velocity = Vector2.down * jumpForce;                 // Move down
        yield return new WaitForSeconds(pattern5DelayTime);     // Falling down 
        rb.gravityScale = gravity;                              // Back to normal gravity after hitting ground
        anim.SetTrigger("JumpReady");
        index++;

        if (index < repeat)
            StartCoroutine(Pattern5(index));        // Repeat
        else
        {
            yield return new WaitForSeconds(endPatternTime);
            anim.SetTrigger("JumpEnd");
            isUsingPattern = false;     // End pattern
            StartCoroutine(StartPattern()); // Start next pattern
        }
    }

    IEnumerator Pattern6()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("pattern 6 (Bat)");

        yield return new WaitForSeconds(pattern3DelayTime);

        yield return new WaitForSeconds(endPatternTime);
        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);      // Attack gizmo
    }
}