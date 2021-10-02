using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    public EnemyUnit ThisUnit;

    private void OnValidate()
    {
        ThisUnit = transform.root.GetComponentInChildren<EnemyUnit>();
    }
}
