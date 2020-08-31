using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInteraction : MonoBehaviour
{
    public float distance = 2.5f;
    public GameObject keyObjectDisplay;
    public GameObject gun;
    public GameObject hud;
    public GameObject weaponHolder;
    public keyRing keys;
    

    // Start is called before the first frame update
    void Start()
    {
        
        keyObjectDisplay.SetActive(false);
    }

    /// <summary>
    /// In the update function, we are firing out a raycast every single frame to detect if there is any object infront
    /// of the player. If the raycast detects an object infront of the player, then it will make sure that the player is
    /// not zooming in. Then it will check the tag of the object, to make sure that the object is interactable and not a static object like a wall.
    /// Next we will check to see if the object the raycast hit has the component of keycard, then we will pickup the keycard.
    /// Otherwise if it is the scar_model (weapon pickup object) then it will delete the scar_model object and unhide the actual gun inside of the weapon holder.
    /// If it is a door we will check to see if it is automatic, if it isn't then we will display an interact overlay on the UI.
    /// </summary>
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance) == true)
        {
            if (gun.transform.GetComponent<GunScript>().isZooming == false)
            {
                if (hit.transform.tag == "Interactable")
                {
                    if (hit.transform.GetComponent<KeyCard>())
                    {
                        hud.transform.GetComponent<HUD>().ChangeText("Keycard", "Pickup (E)");
                        keyObjectDisplay.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            hit.transform.GetComponent<KeyCard>().PickupKey();
                        }
                    }
                    if (hit.transform.name == "scar_model")
                    {
                        hud.transform.GetComponent<HUD>().ChangeText("Scar", "Pickup (E)");
                        keyObjectDisplay.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(hit.transform.gameObject);
                            gun.SetActive(true);
                        }
                    }
                }
                else if (hit.transform.tag == "Door")
                {
                    Door d = hit.transform.parent.GetComponent<Door>();
                    if (d.colour != KeyColour.none)
                    {
                        string upperCase = d.colour.ToString().Substring(0, 1);
                        string restOfWord = d.colour.ToString().Substring(1);
                        hud.transform.GetComponent<HUD>().ChangeText("Door" + " (" + upperCase.ToUpper() + restOfWord + ")", "Interact (E)");
                    }
                    else
                    {
                        hud.transform.GetComponent<HUD>().ChangeText("Door", "Interact (E)");
                    }
                    keyObjectDisplay.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E)) {
                        Animator anim = hit.transform.parent.GetComponent<Animator>();
                        if (d.automatic == false)
                        {
                            if (d.locked == false)
                            {
                                d.ToggleDoor(!anim.GetBool("doorOpen"));
                            }
                            else
                            {
                                if (d.colour == KeyColour.blue)
                                {
                                    if (keys.HasBlueKey() == true)
                                    {
                                        d.ToggleDoor(!anim.GetBool("doorOpen"));
                                    }
                                }
                                else if (d.colour == KeyColour.green)
                                {
                                    if (keys.HasGreenKey() == true)
                                    {
                                        d.ToggleDoor(!anim.GetBool("doorOpen"));
                                    }
                                }
                                else if (d.colour == KeyColour.red)
                                {
                                    if (keys.HasRedKey() == true)
                                    {
                                        d.ToggleDoor(!anim.GetBool("doorOpen"));
                                    }
                                }
                                else if (d.colour == KeyColour.yellow)
                                {
                                    if (keys.HasYellowKey() == true)
                                    {
                                        d.ToggleDoor(!anim.GetBool("doorOpen"));
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    keyObjectDisplay.SetActive(false);
                }
            }
            else
            {
                keyObjectDisplay.SetActive(false);
            }
        }
        else
        {
            keyObjectDisplay.SetActive(false);
        }
    }
}
