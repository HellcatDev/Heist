using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraTrack : MonoBehaviour
{
    // Will be used next semester

    public Transform trackingTarget;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(trackingTarget.transform.position.x, gameObject.transform.position.y, trackingTarget.transform.position.z);
    }
}
