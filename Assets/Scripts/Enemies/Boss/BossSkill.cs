using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public float speed;
    public int damage;
    bool shoot = false;

    [Header("Trail")]
    public GameObject trail;
    public float spawnTime;
    float spawnTimer;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (shoot)
        {
            if (spawnTimer <= 0)
            {
                GameObject temp = Instantiate(trail, transform.position, transform.rotation);
                Destroy(temp, 1f);
                spawnTimer = spawnTime;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        rb.velocity = Vector2.up * speed;
        shoot = true;
        Destroy(gameObject, 5f);        // Destroy after 5 sec
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
