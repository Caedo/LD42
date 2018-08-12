using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody m_Body;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    public void Initialize(WeaponStats stats)
    {
        m_Body.velocity = transform.up * stats.m_BulletSpeed;
    }

}