using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDuckHit : MonoBehaviour
{
    public AudioClip[] hitSound;
    // otherwise on scene load all the ducks 'settle' in the cabinet and make a ton of noise
    private bool isActive = false;

    private void Start()
    {
        int whichSound = Random.Range(0, hitSound.Length);
        GetComponent<AudioSource>().clip = hitSound[whichSound];
    }

    public void SetActive(bool yesOrNo)
    {
        isActive = yesOrNo;
        Debug.Log("set active!");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isActive)
        {
            Debug.Log("adf");
            GetComponent<AudioSource>().Play();
        }
    }
}
