using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    public LayerMask ignoreLayer;

    public bool PlayerInSight(Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, ~ignoreLayer);

        if (hit.collider != null)
        {
            return (hit.collider.tag == "Player");
        }
        else
            return false;
    }
}
