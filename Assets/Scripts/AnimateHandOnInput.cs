using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// deciding grab is grip button. to change this, switch the XRI RightHand Interaction
// to something else. might consider switching to 'at least 50% pressed' as opposed to
// 'is 100% pressed' 

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty grabAnimationAction;
    public InputActionProperty pinchAnimationAction;
    public Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrabbing = grabAnimationAction.action.IsPressed();
        
        if (isGrabbing)
        {
            handAnimator.SetFloat("Grip", 1f);
        }    
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }


        bool isPinching = pinchAnimationAction.action.IsPressed();

        if (isPinching)
        {
            handAnimator.SetFloat("Trigger", 1f);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
    }
}
