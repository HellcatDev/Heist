using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{
    public PocketManager pockets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (pockets.pocketSlots > 0f)
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
