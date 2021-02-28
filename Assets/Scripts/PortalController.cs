using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public int healAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int hp = collision.GetComponent<PlayerController>().hp + healAmount;
            if (hp > 10)
                hp = 10;
            GameController.instance.SaveGame(hp);
            SceneManager.LoadScene(3);
        }
    }
}
