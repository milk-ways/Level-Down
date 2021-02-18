using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    GameObject target;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;

        // Temp destroy after 10 sec
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (target == null)
            {
                target = collision.gameObject;                                      // Get first enemy unit
                target.GetComponent<EnemyController>().TakeDamage(damage);          // Give damage
            }

            Destroy(gameObject);
        }
    }
}
