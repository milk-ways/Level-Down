using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elecfield : EnemyController
{
    [Header("Spark settings")]
    public GameObject spark;
    public float activeTime;            // Time for setting spark on/off
    bool isActive = false;                      // Active: spark on

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        InvokeRepeating("SetActive", activeTime, activeTime);
    }

    void SetActive()
    {
        isActive = !isActive;       // on/off
        spark.SetActive(!spark.activeSelf);

        anim.SetBool("IsActive", isActive);

        //if (gameObject.activeSelf)
        //    gameObject.SetActive(false);
        //else
        //    gameObject.SetActive(true);
    }
}
