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
    [SerializeField] Sprite appearSprite;
    [SerializeField] Sprite hideSprite;
    [SerializeField] bool isAppear;     // True: appear, false: hide

    [Header("Offense")]
    public int offenseDelay;
    [SerializeField] GameObject flowerBullet;

    // Component
    PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        playerInSight = Physics2D.OverlapCircle(transform.position, appearSight, playerLayer);
        playerInhidesight = Physics2D.OverlapCircle(transform.position, hideSight, playerLayer);

        if (!playerInSight || playerInhidesight)
            Hide();     // Stay hiding if player is not in sight

        if (!isAppear)
            if (playerInSight && !playerInhidesight)        // Player is in sight and outside hide sight
                Appear(Dir());
    }

    int Dir()
    {
        if (transform.position.x < player.transform.position.x)
            return 1;       // Player on right
        else
            return -1;      // Player on left
    }

    void Appear(int dir)
    {
        isAppear = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = appearSprite;        // Change to appear sprite

        // Set direction depending on player location
        if (dir == 1)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (dir == -1)
            transform.eulerAngles = new Vector3(0, 180, 0);

        getDamage = true;           // Can get damage from player
        InvokeRepeating("Offense", offenseDelay, offenseDelay);     // Start attacking
    }

    void Hide()
    {
        isAppear = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = hideSprite;      // Change to hide sprite
        getDamage = false;          // Invulnerable
        CancelInvoke("Offense");    // Stop attacking
    }

    // Attack
    void Offense()
    {
        Instantiate(flowerBullet, transform.position, transform.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, appearSight);
        Gizmos.DrawWireSphere(transform.position, hideSight);
    }
}
