using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugColorChange : MonoBehaviour
{
    public Color highlightColor;
    public Color defaultColor;
    public Image background;

    private Animator anim;
    private Image colorChange;
    private bool highlighted;
    // Start is called before the first frame update
    void Start()
    {
        anim = background.GetComponent<Animator>();
        colorChange = gameObject.GetComponent<Image>();
        defaultColor = colorChange.color;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        anim.SetBool("Active", false);
    //        colorChange.color = defaultColor;
    //        highlighted = false;
    //    }
    //}

    /// <summary>
    /// When called, checks if the button is highlighted and then reverses it. If it is false, then it will be true and vise versa.
    /// </summary>
    public void ChangeColor()
    {
        if (highlighted)
        {
            //anim.SetBool("Active", false);
            colorChange.color = defaultColor;
            highlighted = false;
        }
        else
        {
            //anim.SetBool("Active", true);
            colorChange.color = highlightColor;
            highlighted = true;
        }
    }
}
