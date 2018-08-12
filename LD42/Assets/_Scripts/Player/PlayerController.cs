using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private PlayerMovement m_Movement;
	private PlayerShooting m_Shooting;
	private Camera m_Camera;

	private void Awake()
	{
		m_Movement = GetComponent<PlayerMovement>();
		m_Shooting = GetComponent<PlayerShooting>();
		m_Camera = Camera.main;
	}

	private void Update()
	{
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");

		var mousePos = Input.mousePosition;
		var mouseWorldPos = m_Camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -m_Camera.transform.position.z));
		
		m_Movement.Move(new Vector2(h,v));
		m_Movement.SetLookTarget(mouseWorldPos);

		if (Input.GetMouseButton(0))
		{
			m_Shooting.Fire();
		}
	}
}
