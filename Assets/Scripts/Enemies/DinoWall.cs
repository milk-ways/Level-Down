using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            gameObject.GetComponentInParent<Dinosaur>().StopMoving();
        }
    }
}
