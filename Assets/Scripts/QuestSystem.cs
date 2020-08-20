using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    public TMP_Text questTitle;
    public TMP_Text questDescription;
    public string currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        //questTitle = transform.Find("Text").GetComponent<TMP_Text>();
        //questDescription = transform.Find("Description").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSize();
    }

    void StartQuest(string title, string description)
    {
        questTitle.text = title;
        questDescription.text = description;
    }

    void UpdateSize()
    {
        float paddingSize = 8f;
        Vector2 backgroundSize;
        backgroundSize = new Vector2(337f, (26.1f + questDescription.preferredHeight + 31f + 29f + 4.28f) + paddingSize * 2f);
        questTitle.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(304f, 26.1f);
        questDescription.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(304, questDescription.preferredHeight);
        gameObject.GetComponent<RectTransform>().sizeDelta = backgroundSize;
    }
}
