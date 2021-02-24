using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : EnemyController
{
    [Header("Sight")]
    public LayerMask playerLayer;
    public float appearSight;       // Appear sight
    public float hideSight;         // Hide sight
    bool playerInSight = false;     // Player is in sight
    bool playerInhidesight = false; // Player is inside hide sight

    [Header("Sprite")]
    //[SerializeField] Sprite appearSprite;
    //[SerializeField] Sprite hideSprite;
    [SerializeField] bool isAppear;     // True: appeared, false: hiding

    [Header("Offense")]
    public Transform attackPos;
    public int offenseDelay;
    [SerializeField] GameObject flowerBullet;

    // Component
    PlayerController player;
    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        // Start hiding
        Hide();
    }

    void Update()
    {
        playerInSight = Physics2D.OverlapCircle(transform.position, appearSight, playerLayer);
        playerInhidesight = Physics2D.OverlapCircle(transform.position, hideSight, playerLayer);

        FaceDir();

        if (isAppear && (!playerInSight || playerInhidesight))
            Hide();     // Stay hiding if player is not in sight

        if (!isAppear)
            if (playerInSight && !playerInhidesight)        // Player is in sight and outside hide sight
                Appear();
    }

    void FaceDir()
    {
        float dis = player.transform.position.x - transform.position.x;

        //if (-0.05 < dis && dis < 0.05)
        //    return 0;

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            //return 1;
        }
        else //(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            //return -1;
        }
    }

    void Appear()
    {
        anim.SetTrigger("Show");
        isAppear = true;
        getDamage = true;           // Can get damage from player
        InvokeRepeating("Offense", offenseDelay, offenseDelay);     // Start attacking
    }

    void Hide()
    {
        anim.SetTrigger("Hide");
        isAppear = false;
        getDamage = false;          // Invulnerable
        CancelInvoke("Offense");    // Stop attacking
    }

    // Attack
    void Offense()
    {
        Instantiate(flowerBullet, attackPos.position, transform.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, appearSight);
        Gizmos.DrawWireSphere(transform.position, hideSight);
    }
}
