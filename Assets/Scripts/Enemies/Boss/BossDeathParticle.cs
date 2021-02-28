using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathParticle : MonoBehaviour
{
    public GameObject banPanel;

    void Start()
    {
        StartCoroutine(RestartDelay());
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(1);
        Instantiate(banPanel, GameObject.FindGameObjectWithTag("UI").transform);
    }
}
