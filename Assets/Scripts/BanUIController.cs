using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BanUIController : MonoBehaviour
{
    //banimage on button
    [SerializeField] Sprite banimage;
    
    private bool buttonOn = false;  

    [SerializeField] Image[] BanUI; //좌측하단 UI

    private float time = 0;

    PlayerAttack playerAttack;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        for (int i=0; i < 5; i++)
        {
            BanUI[i].enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //버튼을 누르고 2초후에 banpanel 비활성화
        if (buttonOn == true)           
        {
            time += Time.deltaTime;
        }
        if (time >= 2)
        {
            GameObject.Find("BanPanel").SetActive(false);
            time = 0;
            buttonOn = false;
        }
    }

    public void BanJump(Button button)      
    {
        
        if (buttonOn == false)
        {
            //playerController.jumpEnabled = false;         
            button.GetComponent<Button>().image.sprite = banimage;          
            BanUI[0].enabled = true;
            buttonOn = true;
        }
    }
    public void BanDash(Button button)
    {
        if (buttonOn == false)
        {
            //playerController.dashEnabled = false;
            button.GetComponent<Button>().image.sprite = banimage;
            BanUI[1].enabled = true;
            buttonOn = true;
        }
       
        
    }
    public void BanMelee(Button button)
    {
        if (buttonOn == false)
        {
            playerAttack.meleeEnabled = false;
            button.GetComponent<Button>().image.sprite = banimage;
            BanUI[2].enabled = true;
            buttonOn = true;
        }
    }
    public void BanRange(Button button)
    {
        if (buttonOn == false)
        {
            playerAttack.rangeEnabled = false;
            button.GetComponent<Button>().image.sprite = banimage;
            BanUI[3].enabled = true;
            buttonOn = true;
        }
       
    }
    public void BanSkill(Button button)
    {
        if(buttonOn == false)
        {
            playerAttack.skillEnabled = false;
            button.GetComponent<Button>().image.sprite = banimage;
            BanUI[4].enabled = true;
            buttonOn = true;
        }
        
    }
}
