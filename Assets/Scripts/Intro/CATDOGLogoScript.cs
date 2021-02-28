using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CATDOGLogoScript : MonoBehaviour
{
    public GameLogoPlayerScript GameLogoPlayer;
    public Image LogoImage;
    private void Start()
    {
        StartCoroutine(CATDOGLogoCoroutine());
    }

    IEnumerator CATDOGLogoCoroutine()
    {
        for (float t = 0; t < 1.0f; t += Time.deltaTime)
        {
            LogoImage.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), t / 0.5f);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        for (float t = 0; t < 1.0f; t += Time.deltaTime)
        {
            LogoImage.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), t / 0.5f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        GameLogoPlayer.StartAnimation();
        yield return null;
    }
}
