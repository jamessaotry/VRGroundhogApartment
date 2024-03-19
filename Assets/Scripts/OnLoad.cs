using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;


public class OnLoad : MonoBehaviour
{
    public FadeView fade;
    public AudioSource alarm;
    // i'm not sure how to disable controls cleanly. so we have this really stupid solution where
    // we disable the entire right controller object + the footsteps
    public GameObject movement;
    public GameObject footsteps; // 'player'
    void Start()
    {
        if (GameStateManager.Instance.releaseMode)
        {

            // 1) make screen start black
            // 2) freeze player
            fade.InstantOut();
            movement.SetActive(false);
            footsteps.SetActive(false);
            // 3) start the alarm clock after 2 seconds
            StartCoroutine(PlayAlarm(3));
            // 4) cut in, unfreeze player 2 seconds after that
            StartCoroutine(ReturnControl(5));
        }
    }

    private IEnumerator PlayAlarm(int delay)
    {
        yield return new WaitForSeconds(delay);

        alarm.Play();
    }

    private IEnumerator ReturnControl(int delay)
    {
        yield return new WaitForSeconds(delay);

        fade.InstantIn();
        movement.SetActive(true);
        footsteps.SetActive(true);
    }

}
