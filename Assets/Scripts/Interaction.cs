using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject pickupUI;
    public TMP_Text pickupName;
    public TMP_Text pickupWorth;
    public PocketManager pockets;
    public CameraAI cameraSight;
    public CameraAI cameraSight2;
    public GameObject loseScreen;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// In this update function, we cast a raycast and check if the object infront of the player is an interactable item.
    /// If it is, it will check if the cameras can see the player and pickup the item. If the cameras can see the player,
    /// the lose screen is set as active.
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, interactionDistance))
        {
            if (hit.transform.gameObject.GetComponent<Item>())
            {
                pickupUI.SetActive(true);
                pickupName.text = hit.transform.GetComponent<Item>().itemName;
                pickupWorth.text = "Worth: " + hit.transform.GetComponent<Item>().pointScore.ToString();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<Item>().PickupItem();
                    if (cameraSight.canSeePlayer == true || cameraSight2.canSeePlayer == true)
                    {
                        loseScreen.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Debug.Log("Lose");
                    }
                }
            }
            else
            {
                pickupUI.SetActive(false);
            }
        }
        else
        {
            pickupUI.SetActive(false);
        }
    }
}
