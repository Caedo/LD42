using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public ParticleSystem m_DeathParticle;
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

    protected void SpawnParticles()
    {
        if (m_DeathParticle != null)
        {
            Destroy(Instantiate(m_DeathParticle, transform.position, transform.rotation).gameObject,
                m_DeathParticle.main.duration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponentInParent<LivingEntity>();
        if (entity)
        {
            entity.TakeDamage(m_Dmg);
        }

        //Debug.Log(other.gameObject.name);

        SpawnParticles();
        Destroy(gameObject);
    }
}