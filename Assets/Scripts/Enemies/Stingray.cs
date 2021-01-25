using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stingray : EnemyController
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        rigidBody.velocity = (player.transform.position - transform.position).normalized * speed;
    }
}
