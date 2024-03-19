using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TextAsset : ScriptableObject
{
    [TextArea]
    public string text;
}
