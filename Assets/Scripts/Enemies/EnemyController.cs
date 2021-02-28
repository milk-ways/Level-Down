using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemy basic settings
    [Header("Enemy base settings")]
    public int hp;
    // Damage
    public int damage;
    public bool getDamage;             // true:get damage, false:immortal
    public GameObject deathParticle;    // Death particles

    public virtual void TakeDamage(int damage)
    {
        if (getDamage)
        {
            // Recieve damage
            Debug.Log(gameObject.transform.name + " Recieved damage");
            StartCoroutine(TurnRed());
            hp -= damage;
            Camera.main.GetComponent<CamShake>().BigRand();

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
        if (deathParticle != null)
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator TurnRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}