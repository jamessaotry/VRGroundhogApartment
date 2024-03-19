using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    /* 
     * i reconfigured this assuming we want to have a central 'text manager' that
     * knows which one to update and then whatever script needs to 'complete a task'
     * tells the text manager it's done. hopefully this is ok. i think this might
     * be better done with scriptableobjects but it's late and i'm tired
     */

    public enum TaskType
    {
        Phone,
        Breakfast,
        Keys,
        Spooky1,
        Spooky2
    }

    public TMP_Text phoneText;
    public TMP_Text breakfastText;
    public TMP_Text keysText;
    public TMP_Text spookyText1;
    public TMP_Text spookyText2;

    public void UpdateTextDisplay(TaskType whichTask)
    {
        // Update the TMP text
        TMP_Text textDisplay;
        if (whichTask == TaskType.Phone)
        {
            textDisplay = phoneText;
        }
        else if (whichTask == TaskType.Breakfast)
        {
            textDisplay = breakfastText;
        }
        else if (whichTask == TaskType.Keys)
        {
            textDisplay = keysText;
        }
        else if (whichTask == TaskType.Spooky1)
        {
            textDisplay = spookyText1;
        }
        else if (whichTask == TaskType.Spooky2)
        {
            textDisplay = spookyText2;
        }
        else
        {
            Debug.Log("unsupported TaskType in UpdateTextDisplay, brother: " + whichTask);
            return;
        }
        Debug.Log(whichTask);
        textDisplay.text += " Done";
    }
}
