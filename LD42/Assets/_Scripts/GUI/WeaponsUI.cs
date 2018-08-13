using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{

	public Image m_BackgroundImage;
	public List<Text> m_LevelTexts;

	private PlayerShooting m_Shooting;

	private void Start()
	{
		m_Shooting = FindObjectOfType<PlayerShooting>();
	}

	private void Update()
	{
		
	}
}
