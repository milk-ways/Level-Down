using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //hp UI
    public Image[] heart;
    private int heartnum = 5;

    //weapon change
    public Image weapon;
    public Sprite[] weaponTypes;

    //skill cooltime
    [SerializeField] Image skill;
    private bool isCoolTime = false;
    private float currentTime;
    private float delayTime;

    // Components
    PlayerAttack playerAttack;
    PlayerController playerController;

    void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Skill cooltime setting (using PlayerAttack.cs)
        currentTime = playerAttack.skillCoolTime;
        delayTime = playerAttack.skillCoolTime;
    }
 
    void Update()
    {
        //using skill
        if (Input.GetKeyDown(KeyCode.R))
        {
            isCoolTime = true;
        }
        //skill cooltime
        if(isCoolTime)
        {
            currentTime -= Time.deltaTime;
            skill.fillAmount = currentTime/delayTime;
            if(currentTime <= 0)
            {
                isCoolTime = false;
                currentTime = playerAttack.skillCoolTime;
                skill.fillAmount = currentTime / delayTime;
            }
        }
    }

    void LateUpdate()
    {
        //add or remove heart
        if (heartnum > playerController.hp)
        {
            for(int n =0; n < heartnum - playerController.hp; n++)
            {
                heart[heartnum - 1 - n].enabled = false;
            }
            heartnum = playerController.hp;
        }
        else if (heartnum < playerController.hp)
        {
            for(int n =0; n < playerController.hp - heartnum; n++)
            {
                heart[heartnum + n].enabled = true;
            }
            heartnum = playerController.hp;
        }
    }

    public void ChangeWeaponImg(PlayerAttack.AtkType atkType)
    {
        // Weapon change
        if (atkType == PlayerAttack.AtkType.Hand)
            weapon.sprite = weaponTypes[0];
        else if (atkType == PlayerAttack.AtkType.Melee)
            weapon.sprite = weaponTypes[1];
        else
            weapon.sprite = weaponTypes[2];
    }
}
