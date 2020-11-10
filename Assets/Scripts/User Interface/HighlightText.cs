using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HighlightText : MonoBehaviour
{
    public Color highlightColored = Color.red;
    public Color unHighlightColored;

    private TMP_Text theText;

    private void Start()
    {
        theText = gameObject.GetComponent<TMP_Text>();
        unHighlightColored = theText.color;
    }
    public void OnMouseEnter()
    {
        theText.color = highlightColored;
    }
    public void OnMouseExit()
    {
        theText.color = unHighlightColored;
    }
}