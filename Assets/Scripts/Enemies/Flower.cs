using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : EnemyController
{
    [Header("Sight")]
    public float appearSightX;      // X축 시야 범위
    // public float appearSightY;      // Y축 시야
    public float hideSightX;        // 플레이어가 근접할 시 숨는다.
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
    SightController sightController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sightController = GetComponent<SightController>();
    }

    void Update()
    {
        playerInSight = sightController.PlayerInSight(Vector2.right, appearSightX) || sightController.PlayerInSight(Vector2.left, appearSightX);
        playerInhidesight = sightController.PlayerInSight(Vector2.right, hideSightX) || sightController.PlayerInSight(Vector2.left, hideSightX);

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
}
