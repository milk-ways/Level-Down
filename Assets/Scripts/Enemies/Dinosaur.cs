using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : EnemyController
{
    public int moveSec = 0;
    public int moveDir = 0;

    void Start()
    {
        Move();
    }

    void Move()
    {
        Debug.Log("Move");

        moveSec = Random.Range(1, 4);
        Debug.Log(moveSec + "초");

        int newMoveDir = Random.Range(-1, 2);
        while (moveDir == newMoveDir)
        {
            newMoveDir = Random.Range(-1, 2);
        }
        moveDir = newMoveDir;
        Debug.Log(moveDir + "방향");

        if (moveDir == 1)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (moveDir == -1)
            transform.eulerAngles = new Vector3(0, 0, 0);

        Invoke("Move", moveSec);
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(moveDir, 0) * speed;
    }
}