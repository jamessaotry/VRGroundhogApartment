using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabKeys : MonoBehaviour
{
    private XRGrabInteractable grabbable;
    // an audiosource on us doesn't play because the object gets deactivated, lol
    public AudioSource audioSrc; 
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(TakeKeys);
    }

    private void TakeKeys(SelectEnterEventArgs arg)
    {
        Debug.Log("keys grabbed!");
        // tell manager we did it
        GameStateManager.Instance.GrabbedKey();
        // play a jingly sound 
        audioSrc.Play();
        // deactivate ourselves
        gameObject.SetActive(false);
    }
}
