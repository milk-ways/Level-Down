using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathParticle : MonoBehaviour
{
    public GameObject banPanel;

    void Start()
    {
        Instantiate(banPanel, GameObject.FindGameObjectWithTag("UI").transform);
    }
}
