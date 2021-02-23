using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [Header("Movement")]
    public float speed;

    [Header("Attacks")]
    public LayerMask playerLayer;
    public Transform attackPos;
    public float attackRadius;
    public float patternTime;                           // Delay time between patterns
    [SerializeField] bool playerInAtkRange = false;     // Check if player is inside attack range
    [SerializeField] bool isUsingPattern = false;       // True when pattern is enabled

    [Header("Pattern 1")]
    public float pattern1DelayTime;
    public int pattern1Damage;

    [Header("Pattern 2")]
    public GameObject pattern2;
    public Transform pattern2Pos;
    public float pattern2DelayTime;

    [Header("Pattern 3")]
    public float pattern3DelayTime;

    [Header("Pattern 4")]
    public float pattern4DelayTime;
    public int pattern4Damage;

    [Header("Pattern 5")]
    public float pattern5DelayTime;
    public float jumpForce;
    public int repeat;
    float gravity;

    [Header("Pattern 6")]
    public float pattern6DelayTime;

    [Header("Pattern 7")]
    public float pattern7DelayTime;

    // Components
    Rigidbody2D rb;
    PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        gravity = rb.gravityScale;

        StartCoroutine(StartPattern());
    }

    void Update()
    {
        playerInAtkRange = Physics2D.OverlapCircle(attackPos.position, attackRadius, playerLayer);

        if (!isUsingPattern)
        {
            rb.velocity = new Vector2(MoveDir() * speed, rb.velocity.y);        // Move towards player
        }

        if (playerInAtkRange)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
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

        rb.velocity = new Vector2(0, rb.velocity.y);        // Stop moving towards player

        int rand = Random.Range(1, 101);

        // Pattern 1
        if (0 < rand && rand < 21)          // Between 1~20 (20%)
            StartCoroutine(Pattern1());
        else if (20 < rand && rand < 36)    // Between 21~35 (15%)
            StartCoroutine(Pattern2());
        else if (35 < rand && rand < 51)    // Between 36~50 (15%)
            StartCoroutine(Pattern3());
        else if (50 < rand && rand < 71)    // Between 51~70 (20%)
            StartCoroutine(Pattern4());
        else if (70 < rand && rand < 86)    // Between 71~85 (15%)
            StartCoroutine(Pattern5(0));
        else if (85 < rand && rand < 96)    // Between 86~95 (10%)
            StartCoroutine(Pattern6());
        else                                // Between 96~100 (5%)
            StartCoroutine(Pattern7());
    }

    IEnumerator Pattern1()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 1");

        yield return new WaitForSeconds(pattern1DelayTime);
        if (playerInAtkRange)
        {
            player.TakeDamage(pattern1Damage);
        }

        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern2()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 2");

        yield return new WaitForSeconds(pattern2DelayTime);
        Instantiate(pattern2, pattern2Pos.position, Quaternion.identity);

        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern3()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("pattern 3");

        yield return new WaitForSeconds(pattern3DelayTime);

        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern4()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 4");

        yield return new WaitForSeconds(pattern4DelayTime);
        if (playerInAtkRange)
        {
            player.TakeDamage(pattern4Damage);
        }

        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern5(int index)
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("Pattern 4");

        yield return new WaitForSeconds(pattern5DelayTime);
        Vector3 playerPos = player.transform.position;          // Current location of player
        rb.gravityScale = 0;
        rb.velocity = Vector2.up * jumpForce;                   // Move up
        yield return new WaitForSeconds(pattern5DelayTime);     // Going up
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);      // Move to player x position
        rb.velocity = Vector2.down * jumpForce;                 // Move down
        yield return new WaitForSeconds(pattern5DelayTime);     // Falling down 
        rb.gravityScale = gravity;                              // Back to normal gravity after hitting ground
        index++;

        if (index < repeat)
            StartCoroutine(Pattern5(index));        // Repeat
        else
            isUsingPattern = false;     // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern6()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("pattern 6");

        yield return new WaitForSeconds(pattern3DelayTime);

        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    IEnumerator Pattern7()
    {
        isUsingPattern = true;          // Start pattern
        Debug.Log("pattern 7");

        yield return new WaitForSeconds(pattern3DelayTime);

        isUsingPattern = false;         // End pattern
        StartCoroutine(StartPattern()); // Start next pattern
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);      // Attack gizmo
    }
}