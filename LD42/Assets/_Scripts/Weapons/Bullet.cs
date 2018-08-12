using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    protected Rigidbody m_Body;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    public void Initialize(WeaponStats stats)
    {
        m_Body.velocity = transform.up * stats.m_BulletSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<LivingEntity>().TakeDamage(100);
        }
    }
}