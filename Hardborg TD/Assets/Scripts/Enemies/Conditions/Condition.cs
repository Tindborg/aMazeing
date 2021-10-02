using UnityEngine;
using UnityEngine.Events;


public abstract class Condition
{
    public EnemyUnit ThisUnit;
    public ConditionHandler Handler;

    private int _Stacks;
    /**
    private void OnValidate()
    {
        ThisUnit = transform.root.GetComponentInChildren<EnemyUnit>();
    }
    **/
    public virtual void ApplyCondition( int stacks, float duration){}

    public virtual void DeltaCondition(int stacks, float duration) {}

    public virtual void UnApplyCondition()
    {}
}
