using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAI : MonoBehaviour
{
    public float turnSpeed = 4f;
    public GameObject raycaster;
    public bool canSeePlayer = true;

    /// <summary>
    /// Movement is controlled through animations, this script casts a raycast every frame seeing if the player is in the line of sight
    /// of the camera. If true, canSeePlayer is set as true.
    /// </summary>
    void Update()
    {
        Debug.DrawRay(raycaster.transform.position, raycaster.transform.up * 30f, Color.red, 0.1f);
        if (Physics.Raycast(raycaster.transform.position, raycaster.transform.up, out RaycastHit hit, 30f))
        {
            if (hit.transform.tag == "Player")
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else
        {
            canSeePlayer = false;
        }
    }
}
