using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ShowInspectText : MonoBehaviour
{
    /*  
    To make an object inspectable:
    Attach this script to it. (If the object isn't going to move, then this script should be
        attached to an unmoving collider.)
    Create a TextAsset in the Text folder and drag that into Description.
    Set a float offset. Probably some small +y value, but if the object is on a wall then it
        should probably place the text in front of the wall. 
    Attach the XR Grab Interactable script.
     */

    // Locations in the scene of various things we assume we only have one of. 
    // (This is a problem if we ever have two inspect text boxes.)
    private string pathToPlayer = "XR Origin/Camera Offset/Main Camera";
    private Camera player;
    private string pathToInspectCanvas = "InspectText/Canvas";
    private GameObject canvas;
    private string pathToDialogBox = "InspectText/Canvas/Dialog Box";
    private GameObject dialogBox;
    private TMP_Text textElement;

    
    [Tooltip("The TextAsset that should appear when this object is inspected.")]
    public TextAsset description;
    [Tooltip("The positional offset for the floating text on this object.")]
    public Vector3 floatOffset;

    // the transform of some object if the text is active, null if it isn't
    private Transform snappedTo = null;

    // Start is called before the first frame update
    void Start()
    {
        // find all of our objects
        player = GameObject.Find(pathToPlayer).GetComponent<Camera>();
        canvas = GameObject.Find(pathToInspectCanvas);
        dialogBox = GameObject.Find(pathToDialogBox);
        textElement = dialogBox.transform.Find("Text").GetComponent<TMP_Text>();

        XRGrabInteractable grabbed = GetComponent<XRGrabInteractable>();
        // these events fire on pickup and on drop respectively
        grabbed.selectEntered.AddListener(ShowText);
        grabbed.selectExited.AddListener(BanishText);
    }

    public void BanishText(SelectExitEventArgs arg)
    {
        
        snappedTo = null;
        textElement.text = "replace me!";
        dialogBox.SetActive(false);
    }

    public void ShowText(SelectEnterEventArgs arg)
    {
        dialogBox.SetActive(true);
        textElement.text = description.text;
        snappedTo = arg.interactableObject.transform;
        
    }

    public void Update()
    {
        if (snappedTo)
        {
            // move text box
            Vector3 floatPos = snappedTo.position + floatOffset;
            canvas.transform.position = floatPos;
            // rotate to face player. canvas's 'facing' seems to be backwards from desired
            Quaternion rot = Quaternion.LookRotation(
                canvas.transform.position - player.transform.position);
            canvas.transform.rotation = rot;
        }
        
    }
}
