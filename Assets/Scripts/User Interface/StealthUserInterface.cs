using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthUserInterface : MonoBehaviour
{
    public Image stealthBarOne;
    public Image stealthBarTwo;
    [Range(0f, 1f)]
    public float stealthFill;
    public Animator stealthColor;
    public Animator stealthGroup;

    public void UpdateStealth()
    {
        if (stealthBarOne != null)
        {
            stealthFill = PlayerMovementController.alertLevel;
            stealthBarOne.fillAmount = stealthFill;
            stealthBarTwo.fillAmount = stealthFill;
            if (stealthFill <= 0f)
            {
                stealthGroup.SetBool("Visible", false);
            }
            else
            {
                stealthGroup.SetBool("Visible", true);
            }
            if (stealthFill >= 0.8f)
            {
                stealthColor.SetBool("Alerted", true);
            }
            else if (stealthFill < 0.8f)
            {
                stealthColor.SetBool("Alerted", false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStealth();
    }
}
