using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance))
            {
                if (hit.transform.GetComponent<DoorLock>())
                {
                    hit.transform.GetComponent<DoorLock>().StartUnlocking();
                }
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance))
            {
                if (hit.transform.GetComponent<DoorLock>())
                {
                    hit.transform.GetComponent<DoorLock>().Unlocking();
                }
            }
        }
    }
}
