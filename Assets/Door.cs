using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Door : MonoBehaviour
{
    [Range(0f, 100f)]
    public float maxDoorRotation = 95f;
    [Header("Colors")]
    public Material normalColor;
    public Material flashColor;
    public Material openedColor;
    private Animator anim;
    private GameObject doorLock;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.transform.GetComponent<Animator>();
        doorLock = gameObject.transform.Find("DoorLock").gameObject;
    }

    public bool IsLocked()
    {
        if (doorLock.GetComponent<DoorLock>().locked)
        {
            FlashError();
        }
        return doorLock.GetComponent<DoorLock>().locked;
    }

    private void Unlock()
    {
        anim.SetTrigger("Unlocked");
    }

    private void FlashError()
    {
        anim.SetTrigger("Flash");
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.white;
        Handles.DrawWireArc(transform.position, Vector3.up, Vector3.right, maxDoorRotation, 1f);
    }
}
