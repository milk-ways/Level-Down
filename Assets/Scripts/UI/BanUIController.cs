using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BanUIController : MonoBehaviour
{
    //banimage on button
    [SerializeField] Sprite banimage;
    [SerializeField] Image[] banUI; // 버튼 UI

    bool selected = false;

    void Start()
    {
        if (!GameController.instance.jumpEnabled)
            banUI[0].sprite = banimage;
        if (!GameController.instance.dashEnabled)
            banUI[1].sprite = banimage;
        if (!GameController.instance.meleeEnabled)
            banUI[2].sprite = banimage;
        if (!GameController.instance.rangedEnabled)
            banUI[3].sprite = banimage;
        if (!GameController.instance.skillEnabled)
            banUI[4].sprite = banimage;
    }

    IEnumerator Restart()
    {
        GameController.instance.SaveGame(10);

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BanJump()
    {
        if (!selected)
        {
            selected = true;
            GameController.instance.jumpEnabled = false;
            banUI[0].sprite = banimage;
            StartCoroutine(Restart());
        }
    }

    public void BanDash()
    {
        if (!selected)
        {
            selected = true;
            GameController.instance.dashEnabled = false;
            banUI[1].sprite = banimage;
            StartCoroutine(Restart());
        }
    }

    public void BanMelee()
    {
        if (!selected)
        {
            selected = true;
            GameController.instance.meleeEnabled = false;
            banUI[2].sprite = banimage;
            StartCoroutine(Restart());
        }
    }

    public void BanRange()
    {
        if (!selected)
        {
            selected = true;
            GameController.instance.rangedEnabled = false;
            banUI[3].sprite = banimage;
            StartCoroutine(Restart());
        }
    }

    public void BanSkill()
    {
        if (!selected)
        {
            selected = true;
            GameController.instance.skillEnabled = false;
            banUI[4].sprite = banimage;
            StartCoroutine(Restart());
        }
    }
}