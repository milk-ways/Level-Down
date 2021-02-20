using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBullet : MonoBehaviour
{
    public float speed;
    public int damage;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if( collision.tag == "Player")
       {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
       }
    }
}
