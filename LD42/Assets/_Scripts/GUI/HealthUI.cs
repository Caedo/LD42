using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider m_HealthSlider;

    private PlayerHealth m_PlayerHealth;

    private void Start()
    {
        PlayerHealth.Instance.OnPlayerHealthChanged += OnPlayerHealthChanged;
        m_PlayerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnPlayerHealthChanged(float newValue)
    {
        var kappa = m_PlayerHealth.CurrentHealth / m_PlayerHealth.m_MaxHealth;
        Debug.Log("Kappa: " + kappa);
        m_HealthSlider.value = kappa;
    }
}