using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EatCereal : MonoBehaviour
{
    private XRGrabInteractable grabbable;
    public AudioClip crunchingSound;
    public GameObject playerCamera;
    public FadeView fade;

    void Start()
    {
        grabbable = GetComponentInParent<XRGrabInteractable>();
    }


    public void CerealPoured()
    {
        grabbable.selectEntered.AddListener(CompleteBreakfast);
    }

    // complete as in, like, the verb. complete the act of eating breakfast. because i couldn't call
    // it EatCereal() because that's the name of the class. 
    private void CompleteBreakfast(SelectEnterEventArgs arg)
    {
        Debug.Log("CRUNCH CRUNCH CRUNCH");
        grabbable.selectEntered.RemoveAllListeners();
        GameStateManager.Instance.AteBreakfast();
        StartCoroutine(CerealEating());
        
    }

    // i am running out of names for these functions
    private IEnumerator CerealEating()
    {
        // fade to black to hide the cereal carnage
        fade.FadeOut();

        yield return new WaitForSeconds(2);

        // play a noise
        playerCamera.GetComponent<AudioSource>().clip = crunchingSound;
        playerCamera.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(4);

        fade.FadeIn();
    }
}
