using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffAlarm : MonoBehaviour
{
    public AudioSource alarm;
    void Start()
    {
        Button butt = GetComponent<Button>();
        butt.onClick.AddListener(TurnOff);
    }

    public void TurnOff()
    {
        alarm.Stop();
        Debug.Log("poke!");
    }
}
