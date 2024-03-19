using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    // to use this

    // 1. apply script to light we want to make flicker
    // 2. maxFlicker = max of how long flicker should be, maxInterval = max of how long between flickers

    public Light myLight;
    public float maxInterval = 2.0f;
    public float maxFlicker = 0.2f;
    public GameObject lightbulb;
    private Material lightEmission;

    // no nice way for const color...
    private Color offColor = new Color(0.25f, 0.25f, 0.3f);
    // saved color for on
    private Color onColor;


    float defaultIntensity;
    bool isOn;
    float timer;
    float delay;

    private void Start()
    {
        // search through all materials for something that has glass in it's name
        // worst solution of all time
        defaultIntensity = myLight.intensity;
        List<Material> myMaterials = lightbulb.GetComponent<Renderer>().materials.ToList();
        foreach (Material mat in myMaterials)
        {
            Debug.Log(mat.name);
            if (mat.name.Contains("Glass"))
            {
                lightEmission = mat;
            }
        }
        onColor = lightEmission.GetColor("_EmissionColor");
        GetComponent<AudioSource>().Play();

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        isOn = !isOn;

        if (isOn)
        {
            myLight.intensity = defaultIntensity;
            delay = Random.Range(0, maxInterval);
            lightEmission.SetColor("_EmissionColor", onColor);
        }
        else
        {
            myLight.intensity = Random.Range(0.0f, defaultIntensity);
            delay = Random.Range(0, maxFlicker);
            lightEmission.SetColor("_EmissionColor", offColor);
        }

        timer = 0;
    }
}