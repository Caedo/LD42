using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{

	public Transform m_Target;

	public Vector3 offset;

	private void Update()
	{
		transform.position = m_Target.position + offset;
	}
}
