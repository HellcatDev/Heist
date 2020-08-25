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
    [Range(1f, 1.5f)]
    public float staminaRegen = 1f;
    [Range(0.5f, 1f)]
    public float staminaEfficency = 1f;

    [SerializeField]
    private float currentStamina = 0f;
    private float timeSinceLastSprint;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        timeSinceLastSprint = Time.time;
    }

    private void Update()
    {
        UpdateHUD();
        if (Time.time >= timeSinceLastSprint)
        {
            currentStamina += ( 25f * staminaRegen ) * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }

    private void UpdateHUD()
    {
        staminaBar.fillAmount = currentStamina / 100;
        handle.rectTransform.localPosition = new Vector2(-(currentStamina * 4.2f), 0f);
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
        timeSinceLastSprint = Time.time + 1f;
        currentStamina -= ( 25f * staminaEfficency ) * Time.deltaTime;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
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
