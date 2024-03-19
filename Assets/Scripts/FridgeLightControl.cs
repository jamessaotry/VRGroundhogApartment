using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sunbox;

// when the refrigerator door is open, the light should be on
// there's light bleed out the back of the fridge which bothers me but oh well
public class FridgeLightControl : MonoBehaviour
{
    public GameObject fridgeDoor; // whatever has the InteractionBehaviour on it
    public float onIntensity;

    // checking this every frame is super wasteful and a better solution would be to
    // ...idk, use events or something to only check when the door moves. but this is
    // Good Enough for a silly effect that doesn't matter that much
    void Update()
    {
        // this version is for playing in flat
        /*bool isFridgeOpen = fridgeDoor.GetComponent<InteractionBehaviour>()
            .InteractionState == InteractionState.On;
        float intensityShouldBe = isFridgeOpen ? onIntensity : 0;
        if (GetComponent<Light>().intensity != intensityShouldBe)
        {
            GetComponent<Light>().intensity = intensityShouldBe;
        }*/

        // and this version is for physically-simulated door grabbing
        float doorRot = fridgeDoor.transform.rotation.eulerAngles.y;
        // this reading can be intensely jank, so we're trying to clamp it to ~270-359
        if (doorRot < 0 || // unclear why eulerAngles can end up as a tiny negative
            (doorRot < 5 && doorRot >= 0)) // if door phases into fridge, it can be a tiny positive
        {
            doorRot = 359; // assume closed
        }

        // note: eulerAngles tries to avoid negative angles! so an open door doesn't
        // produce the -90 you see in the inspector. assume 355 = -5
        float intensityShouldBe = doorRot < 355 ? onIntensity : 0;
        if (GetComponent<Light>().intensity != intensityShouldBe)
        {
            GetComponent<Light>().intensity = intensityShouldBe;
        }
    }
}
