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

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = (player.transform.position - transform.position).normalized * speed;
    }
}
