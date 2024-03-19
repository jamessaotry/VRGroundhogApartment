using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayFootsteps : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionProperty moveAction;
    public AudioSource footstepClip;

    void Update()
    {
        if (moveAction.action.IsPressed())
        {
            footstepClip.enabled = true;
            //print("moving");
        } else
        {
            footstepClip.enabled = false;
        }
    }
}
