using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactionDistance)) {
                InteractableObject obj = hit.transform.GetComponent<InteractableObject>();
                Debug.Log(obj);
                if (obj != null)
                {
                    obj.Activate();
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance, Color.green, 0.5f);
                }
                else
                {
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance, Color.yellow, 0.5f);
                }
            }
            else
            {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance, Color.red, 0.5f);
            }
        }
    }
}
