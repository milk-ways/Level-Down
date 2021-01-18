using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Player basic settings
    [Header("Basic Settings")]
    public int hp;

    // Player movement related
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
        if (getDamage == true)
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

    public void Die()
    {
        Destroy(gameObject);
    }
}