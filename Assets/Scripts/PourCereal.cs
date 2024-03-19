using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PourCereal : MonoBehaviour
{
    public BoxCollider cerealBoxCollider; // the 'pour' trigger
    public GameObject box; // cereal box itself so we can change the text
    public TextAsset afterPouringText;
    private TMP_Text inspectTextElement;
    private bool hasFired = false;
    
    void Start()
    {
        // look at the stupid garbage i do just to avoid dragging in yet another thing because it feels bad
        GameObject text = GameObject.Find("InspectText/Canvas/Dialog Box/Text");
        inspectTextElement = text.GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasFired && other.Equals(cerealBoxCollider))
        {
            hasFired = true;
            Debug.Log("delicious cereal!");
            GetComponent<AudioSource>().Play();
            // swap out the inspect text
            ShowInspectText inspect = box.GetComponent<ShowInspectText>();
            inspect.description = afterPouringText;
            // this won't actually update it in realtime, so we also need to force an update
            inspectTextElement.text = afterPouringText.text;

            // activate cereal eating capabilities
            GetComponentInParent<EatCereal>().CerealPoured();
        }
    }
}
