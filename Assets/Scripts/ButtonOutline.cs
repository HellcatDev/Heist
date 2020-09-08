using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOutline : MonoBehaviour
{
    private Outline buttonOutline;
    private float outlineFloat;

    [Range(0f, 1f)]
    public float animationIncrement = 0.482f;

    /// <summary>
    /// Button outline communicates with the GameObject that this script is attached to, called gameObject
    /// and gets the outline script built into unity.
    /// </summary>
    void Start()
    {
        buttonOutline = gameObject.GetComponent<Outline>();
    }

    /// <summary>
    /// Every frame the outlineDestination (how think the outline has to be) is calculated using a Linear interpolation.
    /// This allows for a smooth transition between no outline and thick outline, then applies it to the effect distance of the outline.
    /// </summary>
    private void Update()
    {
        float outlineDestination = Mathf.Lerp(buttonOutline.effectDistance.x, outlineFloat, animationIncrement);
        buttonOutline.effectDistance = new Vector2(outlineDestination, outlineDestination);
    }

    /// <summary>
    /// Sets outline amount
    /// </summary>
    /// <param name="outlineAmount"></param>
    public void SetOutline(int outlineAmount)
    {
        outlineFloat = outlineAmount;
    }
}
