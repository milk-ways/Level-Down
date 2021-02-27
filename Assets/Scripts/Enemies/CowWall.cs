using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            gameObject.GetComponentInParent<Cow>().StopMoving();
        }
    }
}
