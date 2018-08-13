using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : LivingEntity
{
    public static event System.Action OnBossDestroyed;

    public float m_AttackRange;
    public CircleFire m_FirstAttack;
    public CircleFire m_SecondAttack;

    private bool m_IsAttacking;
    private bool m_SecoundActivated;
    private Transform m_Player;

    protected override void Die()
    {
        OnBossDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private void Start()
    {
        m_Player = FindObjectOfType<PlayerController>().transform;
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (CurrentHealth < .5 * m_MaxHealth)
        {
            m_SecondAttack.StartAttackWithTimer(m_FirstAttack.Timer);
        }
    }

    private void Update()
    {
        float sqrDist = (transform.position - m_Player.position).sqrMagnitude;
        if (sqrDist < m_AttackRange * m_AttackRange && !m_IsAttacking)
        {
            m_IsAttacking = true;
            m_FirstAttack.StartAttack();
        }
//        else if(!(sqrDist < m_AttackRange * m_AttackRange && !m_IsAttacking))
//        {
//            m_IsAttacking = false;
//            m_FirstAttack.StopAttack();
//            m_SecondAttack.StopAttack();
//        }
    }

}