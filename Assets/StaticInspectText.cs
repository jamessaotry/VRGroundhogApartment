using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticInspectText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnInspect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInspect()
    {
        Debug.Log("inspected!");
    }
}
