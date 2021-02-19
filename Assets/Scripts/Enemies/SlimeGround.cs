using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGround : MonoBehaviour
{
    Slime slime;
    private void Awake()
    {
        slime = GameObject.Find("Slime").GetComponent<Slime>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!slime.canJump)
        {
            slime.canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        slime.canJump = false;
    }
}
