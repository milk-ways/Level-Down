using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Basic settings
    public float speed;
    public int damage;
    public LayerMask enemyLayer;

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

        // Default destroy time 10 sec
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
            StartCoroutine(Attack());
        }

        if (collision.transform.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, transform.GetComponent<SpriteRenderer>().bounds.size, 0, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
