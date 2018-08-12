using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public enum ShotType
{
    Single,
    Spread,
    Shotgun
}

public class Weapon : MonoBehaviour
{
    public Transform m_WeaponPoint;
    public List<WeaponStats> m_WeaponLevels;
    public int m_StartLevel;
    public ShotType m_ShotType;

    [HideInInspector] public int m_CurrentLevel;

    public Bullet m_BulletPrefab;

    private float m_Timer;


    private void Start()
    {
        m_CurrentLevel = m_StartLevel;
    }

    private void Update()
    {
        m_Timer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (m_Timer <= 0)
        {
            switch (m_ShotType)
            {
                case ShotType.Shotgun:
                {
                    for (int i = 0; i < m_WeaponLevels[m_CurrentLevel].m_NumberOfBullets; i++)
                    {
                        float angleZ = Random.Range(-m_WeaponLevels[m_CurrentLevel].m_ShotgunSpreadAngle,
                                m_WeaponLevels[m_CurrentLevel].m_ShotgunSpreadAngle);
                        float angleX = Random.Range(-m_WeaponLevels[m_CurrentLevel].m_ShotgunSpreadAngle,
                            m_WeaponLevels[m_CurrentLevel].m_ShotgunSpreadAngle);
                        var rot = m_WeaponPoint.eulerAngles;
                        rot.z += angleZ;
                        rot.y += angleX;
                        CreateBullet(m_WeaponPoint.position, Quaternion.Euler(rot));
                    }
                    break;
                }
                case ShotType.Single:
                {
                    CreateBullet(m_WeaponPoint.position, m_WeaponPoint.rotation);
                    break;
                }
                case ShotType.Spread:
                {
                    float angle = Random.Range(-m_WeaponLevels[m_CurrentLevel].m_MaxSpreadAngle,
                        m_WeaponLevels[m_CurrentLevel].m_MaxSpreadAngle);
                    var rot = m_WeaponPoint.eulerAngles;
                    rot.z += angle;
                    CreateBullet(m_WeaponPoint.position, Quaternion.Euler(rot));
                    break;
                }
            }
        }
    }

    void CreateBullet(Vector3 position, Quaternion rotation)
    {
        var bullet = Instantiate(m_BulletPrefab, position, rotation);
        bullet.Initialize(m_WeaponLevels[m_CurrentLevel]);
    }
}