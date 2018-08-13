using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    protected Rigidbody m_Body;
    protected float m_Dmg;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    public void Initialize(WeaponStats stats)
    {
        m_Body.velocity = transform.up * stats.m_BulletSpeed;
        m_Dmg = stats.m_DamagePerShot;
    }

    public void Initialize(float speed, float dmg)
    {
        m_Body.velocity = transform.up * speed;
        m_Dmg = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponentInParent<LivingEntity>();
        if (entity)
        {
            entity.TakeDamage(m_Dmg);
        }

        //Debug.Log(other.gameObject.name);
        Destroy(gameObject);
    }
}