using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingChoreo : MonoBehaviour
{
    public FadeView fader;
    void Start()
    {
        // fade in from white on scene load, over 3s
        fader.SetFaderColor();
        fader.InstantOut();
        fader.SetFadeLength(3);
        fader.FadeIn();
    }

}
