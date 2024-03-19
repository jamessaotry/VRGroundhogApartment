using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RingPhone : MonoBehaviour
{
    public GameObject handset;
    private XRGrabInteractable phoneGrab;
    private AudioSource phoneSource;
    // by default, the phone will start ringing x seconds after scene loads
    // not married to this, just needed to get it happening somehow
    public int secondsUntilEvent;

    private void Start()
    {
        // disable the phone being grabbable until it's ringing
        phoneGrab = handset.transform.Find("Phone (Handset)").GetComponent<XRGrabInteractable>();
        phoneSource = handset.GetComponentInChildren<AudioSource>();
        Debug.Log(phoneSource);
        phoneGrab.enabled = false;

        // in SECONDS_UNTIL_EVENT, make the phone ring
        StartCoroutine(TriggerPhoneEvent());
    }

    public void StartRing()
    {
        GetComponent<AudioSource>().Play(); // sound set to loop
        phoneGrab.enabled = true;
        phoneGrab.selectEntered.AddListener(PickupPhone);
    }

    private void PickupPhone(SelectEnterEventArgs arg)
    {
        // turn off the ringing
        GetComponent<AudioSource>().Stop();
        // start playing the call out of the phone
        phoneSource.PlayDelayed(1.5f);
        // kill this listener; don't want this firing twice
        phoneGrab.selectEntered.RemoveAllListeners();
    }

    private IEnumerator TriggerPhoneEvent()
    {
        yield return new WaitForSeconds(secondsUntilEvent);
        StartRing();
    }
}
