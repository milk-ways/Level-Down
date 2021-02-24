using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyController
{
    [Header("Setting")]
    SightController sight;    
    [SerializeField] Rigidbody2D slime_1;
    [SerializeField] Rigidbody2D slime_2;
    public int maxHp;
    public float sightDistance;
    public float speed;
    public float jumpForce;
    public float divideForce;
    public LayerMask layer;

    [Header("State")]
    public bool isMad;
    public bool canJump;
    public bool isJumping;

    Rigidbody2D slimeRigid;
    GameObject player;
    Animator anim;

    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sight = GetComponent<SightController>();
        slimeRigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (sight.PlayerInSight(Vector2.right, sightDistance) || sight.PlayerInSight(-Vector2.right, sightDistance))
            isMad = true;

        if(isMad)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            if (isJumping)
                slimeRigid.velocity = new Vector2(MoveDir() * speed, slimeRigid.velocity.y);        // Move towards player
            else
                slimeRigid.velocity = new Vector2(0, slimeRigid.velocity.y);

            if (canJump)
            {
                StartCoroutine(Jump());
            }            
        }

        if(hp == 2 || hp == 1)
        {
            isMad = true;
        }

        anim.SetBool("CanJump", canJump);
    }

    int MoveDir()
    {
        float dis = player.transform.position.x - transform.position.x;

        if (-0.05 < dis && dis < 0.05)
            return 0;

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return 1;
        }
        else //(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return -1;
        }
    }

    IEnumerator Jump()
    {
        canJump = false;
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(0.3f);
        isJumping = true;
        slimeRigid.velocity = Vector2.up * jumpForce;
    }

    public override void TakeDamage(int damage)
    {
        if (getDamage)
        {
            // Recieve damage
            Debug.Log(gameObject.transform.name + " Recieved damage");

            hp -= damage;
            Camera.main.GetComponent<CamShake>().BigRand();

            if (hp <= 0)
            {
                SlimeDivison();
            }
        }

        else
            Debug.Log("Did not Recieve damage");
    }
    void SlimeDivison()
    {
        Debug.Log("Hi");
        if (maxHp == 4)
        {            
            Rigidbody2D slime_pre1= Instantiate(slime_1, gameObject.transform.position + new Vector3(0.5f,0,0), Quaternion.Euler(0, 0, 0));
            Rigidbody2D slime_pre2 = Instantiate(slime_1, gameObject.transform.position - new Vector3(0.5f, 0, 0), Quaternion.Euler(0, 0, 0));
        }
        if(maxHp == 2)
        {
            Rigidbody2D slime_pre1 = Instantiate(slime_2, gameObject.transform.position + new Vector3(0.5f, 0, 0), Quaternion.Euler(0, 0, 0));
            Rigidbody2D slime_pre2 = Instantiate(slime_2, gameObject.transform.position - new Vector3(0.5f, 0, 0), Quaternion.Euler(0, 0, 0));
        }
        if (deathParticle != null)
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}