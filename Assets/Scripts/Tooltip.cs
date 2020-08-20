using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private TMP_Text tooltipText;
    private RectTransform backgroundRectTransform;
    private Camera uiCamera = null;

    // Start is called before the first frame update
    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<TMP_Text>();
    }

    public void ShowToolTip(string tooltipString)
    {
        gameObject.SetActive(true);
        UpdatePosition();
        tooltipText.text = tooltipString;
        UpdateSize();
    }

    private void Update()
    {
        UpdatePosition();
        UpdateSize();
    }

    private void UpdateSize()
    {
        float textPaddingSize = 4f;
        Vector2 backgroundSize;
        if (tooltipText.preferredWidth > 400)
        {
            tooltipText.rectTransform.sizeDelta = new Vector2(400, tooltipText.preferredHeight);
            backgroundSize = new Vector2(400 + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        }
        else
        {
            tooltipText.rectTransform.sizeDelta = new Vector2(tooltipText.preferredWidth, tooltipText.preferredHeight);
            backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        }
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void UpdatePosition()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        Vector2 modifiedPosition = new Vector2(localPoint.x, localPoint.y + 10f);
        transform.localPosition = modifiedPosition;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
