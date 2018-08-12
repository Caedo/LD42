using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class RocketBullet : Bullet
{
    public float ExplosionForce = 100f;
    public float Explosionradius = 5f;
    public float MaxDamage = 100f;
    private void OnCollisionEnter(Collision other)
    {
        m_Body.AddExplosionForce(ExplosionForce, this.transform.position, ExplosionForce);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, ExplosionForce);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                float damage = MaxDamage * Explosionradius / Vector3.Distance(this.transform.position, collider.bounds.center);
                collider.gameObject.GetComponent<LivingEntity>().TakeDamage(damage);
            }
        }
    }
}
