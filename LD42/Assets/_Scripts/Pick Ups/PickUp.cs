using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    
    [Header("Animation")] 
    public float m_RotationSpeed;
    public Transform m_GraphicsTransform;

    protected abstract void AffectPlayer(PlayerController player);
    
    private void Update()
    {
        m_GraphicsTransform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime, Space.World);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        Debug.Log(player);
        if (player)
        {
            AffectPlayer(player);
            Destroy(gameObject);
        }
    }
}
