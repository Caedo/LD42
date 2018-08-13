using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
    public AudioSource m_DeathSound;
    public float m_MaxHealth;
    public float CurrentHealth { get; protected set; }

    protected virtual void Awake()
    {
        CurrentHealth = m_MaxHealth;
    }

    public virtual void TakeDamage(float dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected void PlayDeathSound()
    {
        if (m_DeathSound != null)
        {
            m_DeathSound.Play();
            m_DeathSound.transform.parent = null;

            Destroy(m_DeathSound.gameObject, m_DeathSound.clip.length);
        }
    }

    protected abstract void Die();
}