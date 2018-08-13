using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerShooting : MonoBehaviour
{
    public Transform m_WeaponGrip;

    public List<Weapon> m_WeaponPrefabs;

    private List<Weapon> m_SpawnedWeapons;
    private Weapon m_CurrentWeapon;
    public int CurrentWeaponIndex { get; private set; }

    private void Awake()
    {
        m_SpawnedWeapons = new List<Weapon>();
        foreach (var weapon in m_WeaponPrefabs)
        {
            var spawned = Instantiate(weapon, m_WeaponGrip);
            spawned.transform.localPosition = Vector3.zero;
            spawned.gameObject.SetActive(false);
            m_SpawnedWeapons.Add(spawned);
        }
        
        m_SpawnedWeapons[0].gameObject.SetActive(true);
        m_CurrentWeapon = m_SpawnedWeapons[0];
    }

    public void SwitchWeapon(int index)
    {
        CurrentWeaponIndex = index;
        m_CurrentWeapon.gameObject.SetActive(false);
        m_CurrentWeapon = m_SpawnedWeapons[index];
        m_CurrentWeapon.gameObject.SetActive(true);
    }

    public int WeaponLevel(int index)
    {
        return m_SpawnedWeapons[index].CurrentLevel;
    }

    public void Fire()
    {
        m_CurrentWeapon.Fire();
    }

    public void UpgradeRandomWeapon()
    {
        var wep = m_SpawnedWeapons.Where(w => w.CurrentLevel != w.MaxLevel).ToList();
        var rand = Random.Range(0, wep.Count);
        wep[rand].Upgrade();
    }
}
