using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUp
{
    public float m_Health;

    protected override void AffectPlayer(PlayerController player)
    {
        player.AddHealth(m_Health);
    }
}