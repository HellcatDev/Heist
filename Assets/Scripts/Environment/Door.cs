using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    private Animator anim;
    private bool open = false;

    public bool locked = false;

    private void Awake()
    {
        anim = gameObject.transform.GetComponent<Animator>();
    }

    public override void Activate()
    {
        if (locked == false)
        {
            if (open)
            {
                anim.SetBool("Opened", false);
                open = false;
            }
            else
            {
                anim.SetBool("Opened", true);
                open = true;
            }
        }
        else
        {
            if (KeyChain.redKey == true)
            {
                if (open)
                {
                    anim.SetBool("Opened", false);
                    open = false;
                }
                else
                {
                    anim.SetBool("Opened", true);
                    open = true;
                }
            }
        }
    }
}
