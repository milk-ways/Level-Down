using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern4Bullet : MonoBehaviour
{
    [Header("Basic settings")]
    public float speed;
    public int damage;
    public float destroyTime;

    [Header("Trail")]
    public GameObject trail;
    public float spawnTime;
    float spawnTimer;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spawnTimer = spawnTime;

        rb.velocity = transform.right * speed;

        Destroy(gameObject, destroyTime);
    }

    void Update()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
