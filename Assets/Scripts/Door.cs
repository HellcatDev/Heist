using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;

    public bool automatic = false;
    //public KeyColour colour = KeyColour.none;
    public bool locked = false;

    /// <summary>
    /// In the awake function we will get the animator component of the object this script is attached to.
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// In this public function, we will be able to toggle the door to be open or closed by setting the animator to whatever toggle is.
    /// </summary>
    public void ToggleDoor(bool toggle)
    {
        anim.SetBool("doorOpen", toggle);
    }

    /// <summary>
    /// In this OnTriggerEnter function we will check the other colliders tag to make sure that it is the player object.
    /// Next we will check if the door is automatic, if it is then it will set the doorOpen parameter to true.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (automatic)
            {
                anim.SetBool("doorOpen", true);
            }
        }
    }

    /// <summary>
    /// In this OnTriggerExit function, we will check to see if the collider is the player object.
    /// If it is then we check if the door is automatic and if the door is automatic, it will set the doorOpen parameter to false, thus closing the door.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (automatic)
            {
                anim.SetBool("doorOpen", false);
            }
        }
    }
}
