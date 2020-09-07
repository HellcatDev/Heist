using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int pointScore = 100;
    public int itemSize = 1;

    private PocketManager itemPockets;
    private Score scoreManager;

    private void Start()
    {
        itemPockets = Camera.main.transform.root.gameObject.GetComponent<Interaction>().pockets;
        scoreManager = Camera.main.transform.root.gameObject.GetComponent<Score>();
    }

    public void PickupItem()
    {
        if (itemPockets.CanPocketsHold(itemSize))
        {
            scoreManager.playerScore += pointScore;
            itemPockets.AddToPockets(itemSize);
            Destroy(gameObject);
        }
    }
}
