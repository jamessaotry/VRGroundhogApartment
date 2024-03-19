using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if i were designing this now i would do this very differently
public class TVControl : MonoBehaviour
{
    // things that need to get animated for each block:
    public PlayAnimOneShot[] jingleActors;
    public PlayAnimOneShot[] newsdeskActors;
    public PlayAnimOneShot[] alertActors;

    // TV components, so we can swap the screen material:
    public GameObject screen;
    public Material screenOff;
    
    // schedule information, ie what to play for this iteration and for how long:
    public TVSchedule tvs;
    private TVSchedule.TVBlock[] currentSchedule;

    void Start()
    {
        currentSchedule = tvs.schedule;
    }

    public void StartTV()
    {
        // we're going to simultaneously launch all our coroutines, so each one needs
        // a delay on the front. yes, this does suck
        Debug.Log("tv started");
        int timeOffset = 0;
        foreach (TVSchedule.TVBlock block in currentSchedule)
        {
            // essentially a big switch on the 'name' of the block to play
            if (block.tvChoreographer.Equals("stationJingle"))
            {
                StartCoroutine(JingleCoroutine(timeOffset, block));
            }
            else if (block.tvChoreographer.Equals("newsDesk"))
            {
                StartCoroutine(NewsdeskCoroutine(timeOffset, block));
            }
            else if (block.tvChoreographer.Equals("emergency"))
            {
                StartCoroutine(EmergencyCoroutine(timeOffset, block));
            }
            timeOffset += block.duration;
        }
        // turn the TV 'off', because we're done
        StartCoroutine(ShutOffCoroutine(timeOffset));
    }

    IEnumerator EmergencyCoroutine(int delay, TVSchedule.TVBlock block)
    {
        Debug.Log("TV: EAS started! waiting " + delay + " seconds to fire");
        yield return new WaitForSeconds(delay);

        // turn the light on (really should've planned for this :/)
        Light screenLight = GameObject.Find("TV Alert Light").GetComponent<Light>();
        screenLight.intensity = 0.8f;
        // swap screen and start the sound
        screen.GetComponent<MeshRenderer>().material = block.screenMat;
        screen.GetComponent<AudioSource>().clip = block.blockAudio;
        screen.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(15);

        // complete a task
        if (GameStateManager.Instance.day == 2)
        {
            GameStateManager.Instance.DoSpookyTask1();
        }

        // start the caption crawl
        foreach (PlayAnimOneShot actor in alertActors)
        {
            actor.PlayAnimation();
        }

        yield return new WaitForSeconds(block.duration - 15);

        // turn the light off, because the screen is being turned off
        screenLight.intensity = 0;
    }

    IEnumerator NewsdeskCoroutine(int delay, TVSchedule.TVBlock block)
    {
        Debug.Log("TV: newsdesk started! waiting " + delay + " seconds to fire");
        yield return new WaitForSeconds(delay);

        // start the actors
        foreach (PlayAnimOneShot actor in newsdeskActors)
        {
            actor.PlayAnimation();
        }
        // swap screen
        screen.GetComponent<MeshRenderer>().material = block.screenMat;
        // play sound
        screen.GetComponent<AudioSource>().clip = block.blockAudio;
        screen.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(block.duration);
        Debug.Log("TV: newsdesk finished!");
    }

    IEnumerator JingleCoroutine(int delay, TVSchedule.TVBlock block)
    {
        Debug.Log("TV: jingle started! waiting " + delay + " seconds to fire");
        yield return new WaitForSeconds(delay);

        
        // start the actors
        foreach (PlayAnimOneShot actor in jingleActors)
        {
            actor.PlayAnimation();
        }
        // from what i understand, this means 'yield to other processes for this long...'
        yield return new WaitForSeconds(3);

        // '...then when that time's up, return to this line:'
        // swap the screen
        screen.GetComponent<MeshRenderer>().material = block.screenMat;
        // play the sound
        screen.GetComponent<AudioSource>().clip = block.blockAudio;
        screen.GetComponent<AudioSource>().Play(); 

        yield return new WaitForSeconds(7);
        Debug.Log("TV: jingle finished!");
    }


    IEnumerator ShutOffCoroutine(int delay)
    {
        Debug.Log("TV: shutoff started! waiting " + delay + " seconds to fire");
        yield return new WaitForSeconds(delay);

        screen.GetComponent<MeshRenderer>().material = screenOff;
        Debug.Log("TV: shutoff finished!");
    }
}
