using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    public LayerMask ignoreLayer;

    public bool PlayerInSight(Vector2 direction, float distance, float yOffset = 0)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z), direction, distance, ~ignoreLayer);

        if (hit.collider != null)
        {
            return (hit.collider.tag == "Player");
        }
        else
            return false;
    }
}
