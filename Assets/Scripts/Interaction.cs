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

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

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
