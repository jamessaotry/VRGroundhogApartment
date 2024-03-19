using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class GrabGameLogic : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(GrabbedBy);
        grabInteractable.selectExited.AddListener(ReleasedBy);
    }

    private void GrabbedBy(SelectEnterEventArgs args)
    {
        var tag = args.interactableObject.transform.tag;
        Debug.Log(tag);
        if (tag.Equals("Phone"))
        {
            // so there's a problem with checking for the day: it's not set correctly if you're
            // doing testing and run a later day from the editor. this is fine for the final
            // release but an issue currently. so i'm bruteforce fixing it with an extra tag
            GameStateManager.Instance.PickedUpPhone();
            
        }
        else if (tag.Equals("SpookyPhone"))
        {
            GameStateManager.Instance.DoSpookyTask2();
        }
    }

    private void ReleasedBy(SelectExitEventArgs args)
    {
        Debug.Log("Released");
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(GrabbedBy);
        grabInteractable.selectExited.RemoveListener(ReleasedBy);
    }
}
