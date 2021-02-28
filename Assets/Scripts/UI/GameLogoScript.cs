using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLogoScript : MonoBehaviour
{
    public RectTransform recttransform;
    Vector3 originalposition;
    void Start()
    {
        originalposition = recttransform.anchoredPosition;
        StartCoroutine(GameLogoAnimation());
    }

    IEnumerator GameLogoAnimation()
    {
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            recttransform.anchoredPosition = originalposition + new Vector3(0, 30.0f * ((float)Math.Sin(t * 1.5f)), 0);
            yield return null;
        }
    }
}
