using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attempt at making a simple all-purpose script for 'this object needs to fire
// exactly one animation on it once'
public class PlayAnimOneShot : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (anim)
        {
            anim.SetTrigger("PlayAnim");
        }
        
    }
}
