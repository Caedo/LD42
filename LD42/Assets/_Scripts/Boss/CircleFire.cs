using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire : MonoBehaviour
{
    public Bullet m_Bullet;
    public int m_NumberOfBullets;
    public float m_BulletInterval;
    public float m_AngleStep;
    public float m_BulletSpeed;
    public float m_BulletDamage;

    public float Timer => m_Timer;
    
    private float m_Timer;
    private int m_AngleStepCounter;

    private bool m_Attack;
    
    public void StartAttack()
    {
        m_Attack = true;
    }

    public void StartAttackWithTimer(float timer)
    {
        m_Timer = timer;
        StartAttack();
    }

    public void StopAttack()
    {
        m_Attack = false;
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_BulletInterval && m_Attack)
        {
            m_Timer = 0;
            m_AngleStepCounter++;
            for (int i = 0; i < m_NumberOfBullets; i++)
            {
                float angle = 360f / m_NumberOfBullets * i + m_AngleStep * m_AngleStepCounter;
                var rotation = Quaternion.Euler(0, 0, angle);
                var bullet = Instantiate(m_Bullet, transform.position, rotation);
                bullet.Initialize(m_BulletSpeed, m_BulletDamage);
            }
        }
    }


}