using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{

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

	protected abstract void Die();
}
