using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public static PlayerHealth Instance { get; private set; }
    
    public event System.Action OnPlayerDeath;
    public event System.Action<float> OnPlayerHealthChanged;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    protected override void Die()
    {
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public override void TakeDamage(float dmg)
    {
        CurrentHealth -= dmg;
        OnPlayerHealthChanged?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        OnPlayerHealthChanged?.Invoke(CurrentHealth);        
    }

    private void OnCollisionEnter(Collision other)
    {
        float collisionSpeed = other.relativeVelocity.magnitude;

        if (collisionSpeed > 9)
        {
            float collisionDamage = (collisionSpeed - 9) * 10;
            Debug.Log(collisionDamage);
            TakeDamage(collisionDamage);
        }
    }
}