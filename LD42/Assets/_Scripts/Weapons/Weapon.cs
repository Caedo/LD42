﻿using System.Collections;
using System.Collections.Generic;
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

    public int CurrentLevel { get; private set; }
    public int MaxLevel => m_WeaponLevels.Count;

    public Bullet m_BulletPrefab;

    private float m_Timer;
    private AudioSource m_Audio;

    private void Awake()
    {
        CurrentLevel = m_StartLevel;
        m_Audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_Timer -= Time.deltaTime;
    }

    public void Upgrade()
    {
        CurrentLevel++;
    }

    public void Fire()
    {
        if (m_Timer <= 0)
        {
            m_Timer = m_WeaponLevels[CurrentLevel].m_TimeBetweenBullets;
            switch (m_ShotType)
            {
                case ShotType.Shotgun:
                {
                    for (int i = 0; i < m_WeaponLevels[CurrentLevel].m_NumberOfBullets; i++)
                    {
                        float angleZ = Random.Range(-m_WeaponLevels[CurrentLevel].m_ShotgunSpreadAngle,
                                m_WeaponLevels[CurrentLevel].m_ShotgunSpreadAngle);
                        float angleX = Random.Range(-m_WeaponLevels[CurrentLevel].m_ShotgunSpreadAngle,
                            m_WeaponLevels[CurrentLevel].m_ShotgunSpreadAngle);
                        var rot = m_WeaponPoint.eulerAngles;
                        rot.z += angleZ;
                        //rot.y += angleX;
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
                    float angle = Random.Range(-m_WeaponLevels[CurrentLevel].m_MaxSpreadAngle,
                        m_WeaponLevels[CurrentLevel].m_MaxSpreadAngle);
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
        if (m_Audio != null)
        {
            m_Audio.Stop();
            m_Audio.Play();
        }
        var bullet = Instantiate(m_BulletPrefab, position, rotation);
        bullet.Initialize(m_WeaponLevels[CurrentLevel]);
    }
}