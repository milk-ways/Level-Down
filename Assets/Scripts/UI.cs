using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //hp UI
    public Image[] heart;
    private int heartnum = 5;

    //weapon change
    public Image[] Weapon;
    public int WeaponNumber = 0;

    //skill cooltime
    [SerializeField] Image skill;
    private bool isCoolTime = false;
    private float currentTime;
    private float delayTime;
    void Start()
    {
        //weapon setting
        Weapon[0].enabled = true;
        Weapon[1].enabled = false;
        Weapon[2].enabled = false;

        //skill cooltime setting (using PlayerAttack.cs)
        PlayerAttack playerattack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        currentTime = playerattack.skillCoolTime;
        delayTime = playerattack.skillCoolTime;
    }

 
    void Update()
    {
        PlayerAttack playerattack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        //weapon change
        if (Input.GetKeyDown(KeyCode.S))
        {
            switch (WeaponNumber)
            {
                case 0:
                    Weapon[0].enabled = false;
                    Weapon[1].enabled = true;
                    WeaponNumber = 1;
                    break;
                case 1:
                    Weapon[1].enabled = false;
                    Weapon[2].enabled = true;
                    WeaponNumber = 2;
                    break;
                case 2:
                    Weapon[2].enabled = false;
                    Weapon[0].enabled = true;
                    WeaponNumber = 0;
                    break;
            }
        }
        
        //using skill
        if(Input.GetKeyDown(KeyCode.R))
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
                currentTime = playerattack.skillCoolTime;
                skill.fillAmount = currentTime / delayTime;
            }
        }

    }
    void LateUpdate()
    {
        //add or remove heart
        PlayerController playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
        if (heartnum > playercontroller.hp)
        {
            for(int n =0; n < heartnum - playercontroller.hp; n++)
            {
                heart[heartnum-1 - n].enabled = false;
            }
            heartnum = playercontroller.hp;
        }
        else if (heartnum < playercontroller.hp)
        {
            for(int n =0; n < playercontroller.hp - heartnum; n++)
            {
                heart[heartnum + n].enabled = true;
            }
            heartnum = playercontroller.hp;
        }
    }
}
