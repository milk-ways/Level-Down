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

    public bool checkDir;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        spear.transform.localRotation = Quaternion.Euler(0, 0, 90);
    }
    private void Update()
    {
        
        inSight =Mathf.Abs(player.transform.position.x - transform.position.x) <= 7.5f;
        anim.SetBool("inSight", inSight);
        if (inSight && checkDir)
        {
            transform.right = new Vector3(player.transform.position.x - transform.position.x, 0, 0);            
        }
    }
    void Check_True()
    {
        checkDir = true;
    }
    void Check_False()
    {
        checkDir = false;
    }
    
}
