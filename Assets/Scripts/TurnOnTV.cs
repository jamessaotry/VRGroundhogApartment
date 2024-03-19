using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTV : MonoBehaviour
{
    public TVControl control;
    private bool hasFired = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!hasFired && other.CompareTag("Player"))
        {
            control.StartTV();
            hasFired = true;
        }
    }
}
