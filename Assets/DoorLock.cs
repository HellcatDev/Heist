using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public float unlockTime = 10f;
    public bool locked = true;

    private float timeSinceUnlock;

    public void StartUnlocking()
    {
        timeSinceUnlock = Time.time;
    }

    public void Unlocking()
    {
        if (Time.time >= timeSinceUnlock + unlockTime)
        {
            locked = false;
        }
    }
}
