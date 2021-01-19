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
    [SerializeField] bool getDamage;             // true:get damage, false:immortal

    void Start()
    {
    }

    void FixedUpdate()
    {
    }

    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        if (getDamage)
        {
            // Recieve damage
            Debug.Log("Recieved damage");

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