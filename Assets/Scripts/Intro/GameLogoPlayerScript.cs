using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameLogoPlayerScript : MonoBehaviour
{
    public RectTransform recttransform;
    public Image Image;
    public Sprite GameLogo;
    Vector3 from, to, FinalLocation;
    // Start is called before the first frame update
    void Start()
    {
        to = recttransform.anchoredPosition;
        FinalLocation = to + new Vector3(-240, 0, 0);
        from = to + new Vector3(0, -900, 0);
        recttransform.anchoredPosition = from;
    }

    public void StartAnimation()
    {
        StartCoroutine(GameLogoPlayerCoroutine());
    }
    IEnumerator GameLogoPlayerCoroutine()
    {
        for (float t = 0; t <= 1.8f; t += Time.deltaTime)
        {
            recttransform.anchoredPosition = Vector3.Lerp(from, to, (float)Math.Sin(t / 1.8f * Math.PI / 2));
            yield return null;
        }
        recttransform.anchoredPosition = to;
        yield return new WaitForSeconds(0.5f);
        Image.sprite = GameLogo;
        yield return new WaitForSeconds(1.0f);

        for (float t = 0; t <= 1.2f; t += Time.deltaTime)
        {
            recttransform.anchoredPosition = Vector3.Lerp(to, FinalLocation, (float)Math.Sin(t / 1.2f * Math.PI / 2));
            yield return null;
        }
        recttransform.anchoredPosition = FinalLocation;

        SceneManager.LoadScene("MainMenu");
    }
}
