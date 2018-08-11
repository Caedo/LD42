using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{

	public Transform m_Target;

	private Vector3 offset;

	private void Start()
	{
		offset = transform.position - m_Target.position;
	}

	private void Update()
	{
		transform.position = m_Target.position + offset;
	}
}
