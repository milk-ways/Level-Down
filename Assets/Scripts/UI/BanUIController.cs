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
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BanJump()
    {
        GameController.instance.jumpEnabled = false;
        StartCoroutine(Restart());
    }

    public void BanDash()
    {
        GameController.instance.dashEnabled = false;
        StartCoroutine(Restart());
    }

    public void BanMelee()
    {
        GameController.instance.meleeEnabled = false;
        StartCoroutine(Restart());
    }

    public void BanRange()
    {
        GameController.instance.rangedEnabled = false;
        StartCoroutine(Restart());
    }

    public void BanSkill()
    {
        GameController.instance.skillEnabled = false;
        StartCoroutine(Restart());
    }
}