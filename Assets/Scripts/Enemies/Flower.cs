using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : EnemyController
{
    [Header("Sight")]
    public int appearSightX; // X축 시야
    public int appearSightY; // Y축 시야
    public int hideSightX; // 플레이어가 근접할 시 숨는다.

    float playerDistX; // X-distance (player <-> flower)
    float playerDistY; // Y-distance (player <-> flower)

    [Header("Sprite")]
    [SerializeField] Sprite appearSprite;
    [SerializeField] Sprite hideSprite;
    [SerializeField] bool isAppear = true;

    [Header("Offense")]
    public int offenseDelay;
    [SerializeField] GameObject flowerBullet;

    void Start()
    {
        Appear(-1);
    }

    void FixedUpdate()
    {
        playerDistY = player.transform.position.y - transform.position.y;

        if (playerDistY > appearSightY || playerDistY < 0) // Y축 시야 밖인 경우
        {
            if (isAppear)
                Hide();
            return;
        }

        playerDistX = player.transform.position.x - transform.position.x;

        if (!isAppear)
        {
            if (playerDistX <= appearSightX && playerDistX > hideSightX) // X축 시야 내, 오른쪽 방향
                Appear(1);

            if (playerDistX >= appearSightX * -1 && playerDistX < hideSightX * -1) // X축 시야 내, 왼쪽 방향
                Appear(-1);

            return;
        }

        if (isAppear)
        {
            if (playerDistX > appearSightX || playerDistX < appearSightX * -1) // X축 시야 밖
                Hide();

            else if (playerDistX <= hideSightX && playerDistX >= hideSightX * -1) // 근접 시 숨음
                Hide();
        }
    }

    void Appear(int dir)
    {
        isAppear = true;

        gameObject.GetComponent<SpriteRenderer>().sprite = appearSprite;

        if (dir == 1)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (dir == -1)
            transform.eulerAngles = new Vector3(0, 180, 0);

        getDamage = true;

        InvokeRepeating("Offense", offenseDelay, offenseDelay);
    }

    void Hide()
    {
        isAppear = false;

        gameObject.GetComponent<SpriteRenderer>().sprite = hideSprite;

        getDamage = false;

        CancelInvoke("Offense");
    }

    void Offense()
    {
        Instantiate(flowerBullet, transform.position, transform.rotation);
    }
}
