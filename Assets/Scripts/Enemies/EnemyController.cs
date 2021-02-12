using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemy basic settings
    [Header("Enemy base settings")]
    public int hp;
    // Damage
    public bool getDamage;             // true:get damage, false:immortal

    public virtual void TakeDamage(int damage)
    {
        if (getDamage)
        {
            // Recieve damage
            Debug.Log(gameObject.transform.name + "Recieved damage");

            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }
        }

        else
            Debug.Log("Did not Recieve damage");
    }

    void Die()
    {
        Destroy(gameObject);
    }
}