using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TowerAimer : MonoBehaviour
{

    private EnemyUnit _target;
    public EnemyUnit Target
    {
        get
        {
            return _target;
        }
        set
        {
            if (value == null)
            {
                if (_target != null)
                {
                    _target.UE_OnRemove.RemoveListener(OnLoseCurrentTarget);
                    UE_OnLoseTarget.Invoke();
                }

                _target = null;
            }
            else
            {
                if (_target == null)
                {
                    value.UE_OnRemove.AddListener(OnLoseCurrentTarget);
                    UE_OnAcquireTarget.Invoke();
                }
                else
                {
                    if (_target != value)
                    {
                        _target.UE_OnRemove.RemoveListener(OnLoseCurrentTarget);
                        value.UE_OnRemove.AddListener(OnLoseCurrentTarget);
                        UE_OnChangeTarget.Invoke();
                    }
                }

                _target = value;
            }
        }
    }

    [Space]

    public UnityEvent UE_OnAcquireTarget;
    public UnityEvent UE_OnChangeTarget;
    public UnityEvent UE_OnLoseTarget;

    //

    private Dictionary<Collider, EnemyUnit> _overlappingUnits = new Dictionary<Collider, EnemyUnit>();

    //private bool _isTracking = false;

    private void OnTriggerEnter(Collider other)
    {
        EnemyUnit enemyUnit = other.transform.root.GetComponent<EnemyUnit>();
        _overlappingUnits[other] = enemyUnit;

        if (Target == null)
        {
            Target = enemyUnit;

            UE_OnAcquireTarget.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _overlappingUnits.Remove(other);

        if (Target != null && Target == other.transform.root)
        {
            OnLoseCurrentTarget();
        }
    }

    private void OnLoseCurrentTarget()
    {
        float closestDistSq = Mathf.Infinity;
        float sqMag;
        Vector2 dxdz;
        EnemyUnit newTarget = null;

        foreach (KeyValuePair<Collider, EnemyUnit> kvp in _overlappingUnits)
        {
            Transform t = kvp.Value.transform;
            
            dxdz = new Vector2(t.position.x, t.position.z)
                - new Vector2(transform.position.x, transform.position.z);

            sqMag = dxdz.sqrMagnitude;
            if (sqMag < closestDistSq)
            {
                closestDistSq = sqMag;
                newTarget = kvp.Value;
            }
        }

        Target = newTarget;
    }

    //private IEnumerator Coroutine_Track()
    //{
    //    _isTracking = true;

    //    while (_currentTarget != null)
    //    {
    //        transform.

    //        yield return null;
    //    }

    //    _isTracking = false;
    //}

}
