using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private bool isInsideHazard = false;
    private float timeSinceLastDamage;
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            isInsideHazard = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            isInsideHazard = false;
        }
    }

    private void Update()
    {
        if (isInsideHazard == true)
        {
            if (Time.time >= timeSinceLastDamage) // If its been 5 seconds since last notification
            {
                timeSinceLastDamage = Time.time + 1f;
                player.GetComponent<Health>().ApplyDamage(25f);
            }
        }
    }
}
