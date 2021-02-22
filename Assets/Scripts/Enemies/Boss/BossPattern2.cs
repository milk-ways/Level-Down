using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern2 : MonoBehaviour
{
    public BossSkill[] bossSkills;
    public float shootDelayTime;
    int shootIndex = 0;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelayTime);
        bossSkills[shootIndex].Shoot();
        shootIndex++;
        if (shootIndex < bossSkills.Length)
            StartCoroutine(Shoot());            // Repeat until every skills are shot
    }

}
