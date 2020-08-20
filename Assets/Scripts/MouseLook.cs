using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 4.0f;
    public float smoothing = 2.0f;
    public bool mouseLookEnabled = true;

    private Vector2 mLook;
    private Vector2 smoothVert;
    private GameObject characterObject;

    // Start is called before the first frame update
    void Start()
    {
        characterObject = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (mouseLookEnabled == true)
        {
            // Locking Cursor to center of screen.
            Cursor.lockState = CursorLockMode.Locked;

            // Getting raw input of the mouse x and mouse y.
            Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Scaling mouseDirection to sensitivity and smoothing values.
            mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            // Stops the camera from being jittery. Creates a smooth transition between frames.
            smoothVert.x = Mathf.Lerp(smoothVert.x, mouseDirection.x, 1f / smoothing);
            smoothVert.y = Mathf.Lerp(smoothVert.y, mouseDirection.y, 1f / smoothing);

            // Adding smoothing to the mLook variable.
            mLook += smoothVert;

            // Clamping to make sure y value doesn't go beyond the 140 looking view.
            mLook.y = Mathf.Clamp(mLook.y, -70, 70);

            // Applying the mouse looking to the rotation of the parent object (using characterObject).
            transform.localRotation = Quaternion.AngleAxis(-mLook.y, Vector3.right);
            characterObject.transform.localRotation = Quaternion.AngleAxis(mLook.x, characterObject.transform.up);

            //Debug.Log(mouseDirection);
        }
    }
}
