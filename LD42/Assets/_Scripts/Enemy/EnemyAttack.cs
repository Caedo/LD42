using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Bullet BulletPrefab;
    public float bullet_interv;
    public float bullet_speed;
    public float bullet_damage;

    private bool attack = false;
    private float timer;

    public void StartAttacking()
    {
        attack = true;
    }

    public void StopAttacking()
    {
        attack = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bullet_interv && attack)
        {
            timer = 0;
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.Initialize(bullet_speed, bullet_damage);
        }
    }
}
