using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class RocketBullet : Bullet
{
    private float m_ExplosionRadius;

    public override void Initialize(WeaponStats stats)
    {
        base.Initialize(stats);

        m_ExplosionRadius = stats.m_ExplosiveRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders =
            Physics.OverlapSphere(this.transform.position, m_ExplosionRadius, LayerMask.GetMask("Enemy"));

        foreach (var collider in colliders)
        {
            var c = collider.GetComponentInParent<LivingEntity>();
            if (c)
            {
                c.TakeDamage(m_Dmg);
            }
        }

        SpawnParticles();
        Destroy(gameObject);
    }
}