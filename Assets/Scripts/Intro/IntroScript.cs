using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public CATDOGLogoScript CATDOGLogo;
    public GameLogoPlayerScript GameLogoPlayer;
    void Start()
    {
        StartCoroutine(IntroCoroutine());   
    }
    IEnumerator IntroCoroutine()
    {
        CATDOGLogo.FadeIn();
        yield return new WaitForSeconds(2.5f);
        CATDOGLogo.FadeOut();
        yield return new WaitForSeconds(1.5f);
        GameLogoPlayer.StartAnimation();
        yield return null;
    }
}
