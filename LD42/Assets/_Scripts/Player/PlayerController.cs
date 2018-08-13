using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_Movement;
    private PlayerShooting m_Shooting;
    private PlayerHealth m_Health;
    private Camera m_Camera;

    private void Awake()
    {
        m_Movement = GetComponent<PlayerMovement>();
        m_Shooting = GetComponent<PlayerShooting>();
        m_Health = GetComponent<PlayerHealth>();
        m_Camera = Camera.main;
    }

    private void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        var mousePos = Input.mousePosition;
        var mouseWorldPos =
            m_Camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -m_Camera.transform.position.z));

        m_Movement.Move(new Vector2(h, v));
        m_Movement.SetLookTarget(mouseWorldPos);

        if (Input.GetMouseButton(0))
        {
            m_Shooting.Fire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Shooting.SwitchWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Shooting.SwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_Shooting.SwitchWeapon(2);
        }
    }

    public void AddHealth(float amount)
    {
        m_Health.Heal(amount);
    }

    public void UpgradeWeapon()
    {
        m_Shooting.UpgradeRandomWeapon();
    }
}