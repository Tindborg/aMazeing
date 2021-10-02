using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionHandler : UnitComponent
{
    private List<Condition> _ActiveConditions = new List<Condition>();

    public void AddCondition( Condition condition, int stacks, float duration )
    {
        bool foundExisting = false;
        foreach (Condition con in _ActiveConditions)
        {
            if (con.GetType() == condition.GetType())
            {
                con.DeltaCondition(stacks, duration);
                foundExisting = true;
                break;
            }
        }
        if (!foundExisting)
        {
            _ActiveConditions.Add(condition);
            condition.ApplyCondition(stacks, duration);
        }
    }

    public void RemoveCondition( Condition condition )
    {
        condition.UnApplyCondition();
        _ActiveConditions.Remove(condition);
    }

    [ContextMenu("Add Slow")]
    public void DebugAddSlow()
    {
        SlowCondition newCon = new SlowCondition();
        newCon.ThisUnit = ThisUnit;
        newCon.Handler = this;
        AddCondition(newCon, 1, 3f);
    }
}
