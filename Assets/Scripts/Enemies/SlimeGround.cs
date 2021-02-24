using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGround : MonoBehaviour
{
    Slime slime;

    void Start()
    {
        slime = transform.parent.GetComponent<Slime>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!slime.canJump)
        {
            slime.canJump = true;
            slime.isJumping = false;
            slime.movingToPlayer = false;
        }
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    slime.canJump = false;
    //}
}
