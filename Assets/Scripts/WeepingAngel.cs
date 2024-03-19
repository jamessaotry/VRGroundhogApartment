using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeepingAngel : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public AudioSource footstepsClip;
    Vector3 dest;
    public Camera playerCam;
    public float aiSpeed;
    public float minDistance;

    // Update is called once per frame
    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        Bounds bounds = GameObject.Find("Body").GetComponent<Renderer>().bounds;

        if (GeometryUtility.TestPlanesAABB(planes, bounds)
            || Vector3.Distance(transform.position, player.position) < minDistance)
        {
            ai.speed = 0;
            ai.SetDestination(transform.position);
            footstepsClip.enabled = false;
        }
        else
        {
            ai.speed = aiSpeed;
            dest = player.position;
            ai.destination = dest;
            footstepsClip.enabled = true;
        }
    }
}
