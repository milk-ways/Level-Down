using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CATDOGLogoScript : MonoBehaviour
{
    public Image LogoImage;
    public void FadeIn()
    {
        StartCoroutine(AnimationFadeIn());
    }
    public void FadeOut()
    {
        StartCoroutine(AnimationFadeOut());
    }
    IEnumerator AnimationFadeIn()
    {
        for (float t = 0; t < 1.0f; t += Time.deltaTime)
        {
            LogoImage.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), t / 0.5f);
            yield return null;
        }
    }
    IEnumerator AnimationFadeOut()
    {
        for (float t = 0; t < 1.0f; t += Time.deltaTime)
        {
            LogoImage.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), t / 0.5f);
            yield return null;
        }
    }
}
