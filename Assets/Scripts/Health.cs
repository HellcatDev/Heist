using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public Image healthbar;
    public Image healthEffect;
    public Image handle;
    [Range(0f, 1f)]
    public float animationIncrement;

    [SerializeField]
    private float currentHealth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        UpdateHUD();
        if (Input.GetKeyDown(KeyCode.L))
        {
            ApplyDamage(20);
        }
    }

    private void UpdateHUD()
    {
        healthbar.fillAmount = currentHealth / 100;
        handle.rectTransform.localPosition = new Vector2(-(currentHealth * 5), 0);
        if (currentHealth/100 < healthEffect.fillAmount)
        {
            healthEffect.fillAmount = Mathf.Lerp(healthEffect.fillAmount, currentHealth / 100, animationIncrement);
        }
        else
        {
            healthEffect.fillAmount = currentHealth / 100;
        }
    }

    public void ApplyDamage(float damageValue)
    {
        currentHealth -= damageValue;
        Debug.Log(currentHealth);
        if (currentHealth < 0)
        {
            currentHealth = 0;
            // Death function
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
