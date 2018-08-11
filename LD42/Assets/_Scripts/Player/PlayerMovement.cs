using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float m_Speed;

    private Rigidbody m_Body;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 move)
    {
        m_Body.AddForce(move * m_Speed);
    }

    public void SetLookTarget(Vector3 point)
    {
        var dir = point - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        
        Debug.Log(point);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}