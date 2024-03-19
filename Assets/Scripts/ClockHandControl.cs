using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandControl : MonoBehaviour
{
    public Transform secondHand;
    public Transform minuteHand;
    public Transform hourHand;
    public bool spookyTime; // whether the hands should spin
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spookyTime)
        {
            secondHand.Rotate(0, 0, -3f);
            minuteHand.Rotate(0, 0, -2f);
            hourHand.Rotate(0, 0, -1f);
        }
        
    }
}
