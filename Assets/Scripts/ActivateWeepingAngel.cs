using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeepingAngel : MonoBehaviour
{
    public GameObject weepingAngel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weepingAngel.SetActive(true);
        }
    }
}
