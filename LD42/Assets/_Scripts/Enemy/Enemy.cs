using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : LivingEntity
{
    public float speed;
    public float max_distance;
    public float min_shooting_distance;
    public float min_distance;

    public EnemyAttack EnemyWeapon1;

    private bool foundPlayer;
    private bool is_attacking = false;
    Transform player;               // Reference to the player's position.

    protected override void Die()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(float dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Awake ()
    {
        base.Awake();
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p)
        {
            foundPlayer = true;
            player = p.transform;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!foundPlayer)
            return;
        
        float step = speed * Time.deltaTime;
        float distance = Vector3.Distance(player.position, transform.position);

        Vector3 direction = player.position - transform.position;

        if (distance >= min_distance && distance <= max_distance)
        {
            var dir = player.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            GetComponent<Rigidbody>().AddForce(direction * step);
        }

        is_attacking = distance <= min_shooting_distance ? true : false;

        if(is_attacking)
        {
            EnemyWeapon1.StartAttacking();
        }
        else
        {
            EnemyWeapon1.StopAttacking();
        }
	}
}
