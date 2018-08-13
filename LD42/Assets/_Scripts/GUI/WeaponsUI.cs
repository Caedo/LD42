using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{

	public Image m_BackgroundImage;
	public List<Text> m_LevelTexts;
	public List<Transform> m_Kappas;

	private PlayerShooting m_Shooting;

	private void Start()
	{
		m_Shooting = FindObjectOfType<PlayerShooting>();
	}

	private void Update()
	{
		for (int i = 0; i < m_LevelTexts.Count; i++)
		{
			m_LevelTexts[i].text = $"Lv {m_Shooting.WeaponLevel(i) + 1}";
		}

		m_BackgroundImage.transform.position = m_Kappas[m_Shooting.CurrentWeaponIndex].position;
	}
}
