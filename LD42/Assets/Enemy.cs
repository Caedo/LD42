using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public float speed;
    public float max_distance;
    public float min_distance;
    Transform player;               // Reference to the player's position.

    // Use this for initialization
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
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
	}
}
