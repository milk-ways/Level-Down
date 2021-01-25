using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elecfield : EnemyController
{
    void Start()
    {
        InvokeRepeating("SetActive", 1, 1);
    }

    void SetActive()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
