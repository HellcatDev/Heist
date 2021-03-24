using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject
{
    public override void Activate()
    {
        KeyChain.redKey = true;

        gameObject.SetActive(false);
    }
}
