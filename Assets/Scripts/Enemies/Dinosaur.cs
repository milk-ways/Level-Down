using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : EnemyController
{
    public int moveSec = 0;
    public int moveDir = 0;

    public bool fromBehind;

    void Start()
    {
        Move();
    }

    void Move()
    {
        moveSec = Random.Range(1, 4);

        int newMoveDir = Random.Range(-1, 2);

        while (moveDir == newMoveDir)
        {
            newMoveDir = Random.Range(-1, 2);
        }

        moveDir = newMoveDir;

        if (moveDir == 1)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (moveDir == -1)
            transform.eulerAngles = new Vector3(0, 0, 0);

        Invoke("Move", moveSec);
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDir * speed, rigidBody.velocity.y);
    }

    public override void TakeDamage(int damage)
    {
        // Only get damage if attacked from behind
        if (fromBehind)
            base.TakeDamage(damage);
    }
}