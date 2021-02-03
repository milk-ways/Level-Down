using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Basic settings
    public float speed;
    public int damage;

    // Trail
    [Header("Trail")]
    public GameObject trail;
    public float spawnTime;               // Spawn timer
    [SerializeField] float spawnTimer;    // Spawn timer countdown

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnTimer = spawnTime;

        rb.velocity = transform.right * speed;

        // Temp destroy after 10 sec
        Destroy(gameObject, 10f);
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
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
