using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ShowDoorUI : MonoBehaviour
{
    public GameObject doorUI;


    private void Start()
    {
        doorUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            doorUI.SetActive(true);
        }
        Debug.Log("Entered door trigger zone");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorUI.SetActive(false);
        }
        /*Debug.Log("Left door trigger zone");*/
    }
}