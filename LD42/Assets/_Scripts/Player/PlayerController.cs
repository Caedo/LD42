using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private PlayerMovement m_Movement;

	private void Awake()
	{
		m_Movement = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		
		m_Movement.Move(new Vector2(h,v));
	}
}
