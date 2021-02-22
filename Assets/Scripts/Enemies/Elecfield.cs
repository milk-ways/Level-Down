using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elecfield : EnemyController
{
    [Header("Spark settings")]
    public GameObject spark;
    public float activeTime;            // Time for setting spark on/off
    int damageVal;
    bool isActive = false;                      // Active: spark on

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        damageVal = damage;

        InvokeRepeating("SetActive", activeTime, activeTime);
    }

    void SetActive()
    {
        isActive = !isActive;       // on/off
        spark.SetActive(!spark.activeSelf);

        if (isActive)
            damage = damageVal;
        else
            damage = 0;

        anim.SetBool("IsActive", isActive);
    }
}
