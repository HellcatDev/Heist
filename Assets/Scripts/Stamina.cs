using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public Image staminaBar;
    public Image staminaEffect;
    public Image handle;
    [Range(0f, 1f)]
    public float animationIncrement;

    [SerializeField]
    private float currentStamina = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        UpdateHUD();
        DrainStamina(10f);
    }

    private void UpdateHUD()
    {
        staminaBar.fillAmount = currentStamina / 100;
        handle.rectTransform.localPosition = new Vector2(-(currentStamina * 3.1f), 0f);
        if (currentStamina / 100 < staminaEffect.fillAmount)
        {
            staminaEffect.fillAmount = Mathf.Lerp(staminaEffect.fillAmount, currentStamina / 100, animationIncrement);
        }
        else
        {
            staminaEffect.fillAmount = currentStamina / 100;
        }
    }

    public void Run()
    {

    }

    public void DrainStamina(float value)
    {
        currentStamina -= value;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }
}
