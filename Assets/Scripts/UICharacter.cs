using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICharacter : MonoBehaviour
{
    public float rotateSpeed = 5f;
    [Range(0f, 2f)]
    public float slowDownSpeed = 0.98f;
    public GameObject character;

    private bool isOverObject = false;
    private float mouseInput;
    private bool isDragging = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isOverObject)
            {
                isDragging = true;
            }
            if (isDragging)
            {
                mouseInput = Input.GetAxis("Mouse X") * -1 * rotateSpeed;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
            }
        }
        mouseInput *= slowDownSpeed;
        character.transform.Rotate(new Vector3(0.0f, Mathf.Lerp(character.transform.localRotation.y, mouseInput, 1.5f), 0.0f));
    }

    public void MouseOver()
    {
        isOverObject = true;
    }

    public void MouseNotOver()
    {
        isOverObject = false;
    }
}
