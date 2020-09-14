using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TextHighlightHover : MonoBehaviour
{
    public Color highlightColor;
    public Color unhighlightedColor;
    public bool button = true;

    //private EventTrigger textEventTrigger;
    private TMP_Text theText;

    private void Start()
    {
        theText = gameObject.GetComponent<TMP_Text>();
        unhighlightedColor = theText.color;
        //theText.transform.gameObject.AddComponent<EventTrigger>();
        //textEventTrigger = gameObject.GetComponent<EventTrigger>();
        //textEventTrigger.OnPointerEnter = gameObject.transform.GetComponent<TextHighlightHover>().MouseEnter();

    }
    public void MouseEnter()
    {
        if (button)
        {
            Button buttonObj = gameObject.transform.parent.gameObject.GetComponent<Button>();
            if (buttonObj.interactable)
            {
                theText.color = highlightColor;
            }
        }
        else
        {
            theText.color = highlightColor;
        }
    }
    public void MouseExit()
    {
        if (button)
        {
            Button buttonObj = gameObject.transform.parent.gameObject.GetComponent<Button>();
            if (buttonObj.interactable)
            {
                theText.color = unhighlightedColor;
            }
        }
        else
        {
            theText.color = unhighlightedColor;
        }
    }
}