using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            gameObject.GetComponentInParent<Turtle>().StopMoving();
        }
    }
}
