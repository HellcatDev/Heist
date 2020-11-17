using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableDoor : MonoBehaviour
{
    private Vector2 mouseStart;
    private float startRot;

    public GameObject activeEditObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Input.mousePosition;
            startRot = activeEditObject.transform.rotation.y;
        }

        if (Input.GetMouseButton(0))
        {
            float deltaX = (mouseStart.x - Input.mousePosition.x) / 3f;
            activeEditObject.transform.eulerAngles = new Vector3(activeEditObject.transform.eulerAngles.x, deltaX + startRot, activeEditObject.transform.eulerAngles.z);
        }
    }
}
