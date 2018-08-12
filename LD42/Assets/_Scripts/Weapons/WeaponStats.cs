using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
    public float m_DamagePerShot;
    public float m_TimeBetweenBullets;
    public float m_BulletSpeed;

    [Header("Spread Settings")] public float m_MaxSpreadAngle;
    [Header("Explosive Settings")] public float m_ExplosiveRange;
    [Header("Shotgun Settings")] public int m_NumberOfBullets;
}