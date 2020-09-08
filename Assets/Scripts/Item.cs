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

    /// <summary>
    /// Gets components so I don't have to manually assign 1000 references for 500 different items
    /// </summary>
    private void Start()
    {
        itemPockets = Camera.main.transform.root.gameObject.GetComponent<Interaction>().pockets;
        scoreManager = Camera.main.transform.root.gameObject.GetComponent<Score>();
    }

    /// <summary>
    /// Checks if the pockets can hold the size of the item, if true, the item is then picked up and destroyed in the scene.
    /// </summary>
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
