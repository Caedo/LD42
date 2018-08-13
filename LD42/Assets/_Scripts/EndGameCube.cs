using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCube : LivingEntity
{

    public event System.Action OnCubeDestroyed;
    
    protected override void Die()
    {
        OnCubeDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
