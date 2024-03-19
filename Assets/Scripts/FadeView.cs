using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeView : MonoBehaviour
{
    private float FADE_LENGTH = 1.5f; // in seconds

    // we make this public and set it in the editor to avoid a race condition where it's not
    // assigned yet when we try to call InstantOut at scene load
    public Renderer fadeSurface;

    public void FadeIn()
    {
        StartCoroutine(FadeLerp(1, 0));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeLerp(0, 1));
    }


    // ugly fix to make it so fade length is correct for the end of the game and we don't have to change
    // other code
    public void SetFadeLength(float length)
    {
        FADE_LENGTH = length;
    }

    public void SetFaderColor()
    {
        Color newColor = fadeSurface.material.GetColor("_BaseColor");
        newColor.a = 0;
        newColor.r = 1.0f;
        newColor.g = 1.0f;
        newColor.b = 1.0f;
        fadeSurface.material.SetColor("_BaseColor", newColor);
    }

    // this and InstantIn are for doing an 'instant' fade, if you need an immediate cut
    public void InstantOut()
    {
        Debug.Log(fadeSurface);
        Color newColor = fadeSurface.material.GetColor("_BaseColor");
        newColor.a = 1;
        fadeSurface.material.SetColor("_BaseColor", newColor);
    }

    public void InstantIn()
    {
        Color newColor = fadeSurface.material.GetColor("_BaseColor");
        newColor.a = 0;
        fadeSurface.material.SetColor("_BaseColor", newColor);
    }

    private IEnumerator FadeLerp(float start, float end)
    {
        float timeElapsed = 0;

        while (timeElapsed < FADE_LENGTH)
        {
            Color newColor = fadeSurface.material.GetColor("_BaseColor");
            newColor.a = Mathf.Lerp(start, end, timeElapsed / FADE_LENGTH);
            fadeSurface.material.SetColor("_BaseColor", newColor);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Color finalColor = fadeSurface.material.GetColor("_BaseColor");
        finalColor.a = end;
        fadeSurface.material.SetColor("_BaseColor", finalColor);
    }
}
