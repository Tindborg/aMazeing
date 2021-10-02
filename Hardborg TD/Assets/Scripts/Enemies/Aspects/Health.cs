using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : UnitComponent
{
    public float MaxHealth;
    public float CurrentHealth;
    
    public void DealDamage( float damage )
    {

    }

    public float GetHealth()
    {
        return CurrentHealth;
    }
}
