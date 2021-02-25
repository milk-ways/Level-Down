using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearmoai : EnemyController
{
    [SerializeField] SightController sight;
    public bool inSight;
    public Animator anim;
    [SerializeField] GameObject spear;
    GameObject player;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sight = GetComponent<SightController>();
        anim = GetComponent<Animator>();
        spear.transform.localRotation = Quaternion.Euler(0, 0, 90);
    }
    private void Update()
    {
        inSight = sight.PlayerInSight(Vector2.right, 7.5f) || sight.PlayerInSight(-Vector2.right, 7.5f);
        anim.SetBool("inSight", inSight);
        if (inSight)
        {
            transform.right = new Vector3(player.transform.position.x - transform.position.x, 0, 0);

            
        }
    }

    
}
