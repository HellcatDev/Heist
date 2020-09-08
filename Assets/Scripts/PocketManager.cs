using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PocketManager : MonoBehaviour
{
    public float pocketPercentage;
    public float pocketSlots;
    public TMP_Text pocketStatusText;
    public TMP_Text pocketPercentageText;
    public TMP_Text pocketSlotsText;
    public Color pocketSpaceUnavaliableColor;
    public float maxPocketSlots = 24;

    // Start is called before the first frame update
    void Start()
    {
        pocketStatusText.text = "Space avaliable";
        pocketPercentage = 0f;
        pocketSlots = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pocketPercentageText.text = pocketPercentage.ToString() + "%";
        pocketSlotsText.text = pocketSlots.ToString() + "/" + maxPocketSlots.ToString();
    }

    /// <summary>
    /// Returns false is the pockets cannot hold the amount specified, returns true if it can hold the amount specified.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CanPocketsHold(int amount)
    {
        if (pocketSlots + amount > maxPocketSlots)
        {
            return false;
        } else
        {
            return true;
        }
    }

    /// <summary>
    /// Adds the amount specified to the players pockets if the pockets have enough space. If pockets do not have space,
    /// it will update the UI.
    /// </summary>
    /// <param name="amount"></param>
    public void AddToPockets(int amount)
    {
        if (pocketSlots + amount <= maxPocketSlots)
        {
            pocketSlots += amount;
            pocketPercentage = Mathf.Round((pocketSlots / maxPocketSlots) * 100);
            pocketStatusText.text = "Space avaliable";
        }
        if (pocketSlots == maxPocketSlots)
        {
            pocketStatusText.text = "Pockets full";
            pocketStatusText.color = pocketSpaceUnavaliableColor;
        }
    }
}
