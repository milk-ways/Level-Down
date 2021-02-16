using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemy basic settings
    [Header("Enemy base settings")]
    public int hp;
    // Damage
    public int damage;
    public bool getDamage;             // true:get damage, false:immortal

    // Attack
    public PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (getDamage)
        {
            // Recieve damage
            Debug.Log(gameObject.transform.name + " Recieved damage");

            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }
        }

        else
            Debug.Log("Did not Recieve damage");
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        player.TakeDamage(damage);
    //    }
    //}

    void Die()
    {
        Destroy(gameObject);
    }
}