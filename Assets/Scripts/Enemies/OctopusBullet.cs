using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBullet : MonoBehaviour
{
    public int damage;
    Collider2D bulletCollider;
    private void Start()
    {
        bulletCollider = GetComponent<Collider2D>();

    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if( collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
