using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetPokeToFingerAttachPoint : MonoBehaviour
{   
    // set attach point we want the poke interactor to have
    // in the inspector pane
    public Transform PokeAttachPoint;

    // this will be set through navigating the hierarchy at runtime
    private XRPokeInteractor _xrPokeInteractor;

    
    void Start()
    {
        _xrPokeInteractor = transform.parent.GetComponentInChildren<XRPokeInteractor>();
        SetPokeAttachPoint();
    }

    // Set attach point of the poke interactor we found
    void SetPokeAttachPoint()
    {
        if (PokeAttachPoint == null)
        {
            Debug.Log("Poke Attach Point is null"); return;
        }

        if (_xrPokeInteractor == null)
        {
            Debug.Log("XR Poke Interactor is null"); return;
        }

        _xrPokeInteractor.attachTransform = PokeAttachPoint;
    }

}
