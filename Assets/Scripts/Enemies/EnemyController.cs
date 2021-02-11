using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemy basic settings
    [Header("Basic Settings")]
    public int hp;

    // Enemy movement related
    [Header("Movement")]
    public float speed;                             // Move speed

    [Header("Get Damage")]
    public bool getDamage;             // true:get damage, false:immortal

    public Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (getDamage)
        {
            // Recieve damage
            Debug.Log(gameObject.transform.name + "Recieved damage");

            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }
        }

        else
            Debug.Log("Did not Recieve damage");
    }

    void Die()
    {
        Destroy(gameObject);
    }
}