using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SmallR()
    {
        anim.SetTrigger("SmallR");
    }

    public void SmallL()
    {
        anim.SetTrigger("SmallL");
    }

    public void SmallRand()
    {
        int rand = Random.Range(0, 8);      // Rand for random shakes
        anim.SetFloat("SmallRandVal", (float)rand);
        anim.SetTrigger("SmallRand");
    }

    public void BigRand()
    {
        int rand = Random.Range(0, 2);      // Rand for random shakes
        anim.SetFloat("BigRandVal", (float)rand);
        anim.SetTrigger("BigRand");
    }

    public void PlayerDie()
    {
        anim.SetTrigger("PlayerDie");
    }
}
