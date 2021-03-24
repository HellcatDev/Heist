using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppear : MonoBehaviour
{
    public TMP_Text text;
    private Collider col;

    private void Start()
    {
        col = gameObject.GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        text.gameObject.SetActive(true);
    }
}
