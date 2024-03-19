using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class TVSchedule : ScriptableObject
{
    [System.Serializable]
    public struct TVBlock
    {
        public int duration; // in seconds
        // which material should be applied to the screen for this block
        public Material screenMat;
        // which sound should be played during this block
        public AudioClip blockAudio;
        // 'which program to run'; determines which function TVControl runs to make this block happen
        public string tvChoreographer;
    }

    public int iteration; // which 'day'/'loop' is this schedule for?
    public TVBlock[] schedule; // a schedule is a bunch of blocks we execute in order, then turn the tv off
}
