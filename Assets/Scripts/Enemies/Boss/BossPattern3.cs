using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern3 : MonoBehaviour
{
    public GameObject particle;
    public int damage;
    bool attackAble = false;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Shot()
    {
        anim.SetTrigger("Shot");
        GameObject temp = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(temp, 2f);
        attackAble = true;
    }

    public void End()
    {
        anim.SetTrigger("End");
    }

    public void Des()
    {
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && attackAble)
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
