using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCondition : Condition
{
    public float SlowPercent = 0.5f;
    private float _DurationLeft;

    private Navigator _Nav;
    public override void ApplyCondition(int stacks, float duration)
    {
        base.ApplyCondition(stacks, duration);
        _Nav = ThisUnit.MyNavigator;
        _Nav.UE_UpdateMoveSpeed.AddListener(ApplySlow);
        _DurationLeft = duration;
        _Nav.UpdateSpeed();
        GameManager.Instance.StartCoroutine(Coroutine_Duration());
    }

    public override void DeltaCondition(int stacks, float duration)
    {
        base.DeltaCondition(stacks, duration);
        _DurationLeft += duration;
    }

    public override void UnApplyCondition()
    {
        base.UnApplyCondition();
        _Nav.UE_UpdateMoveSpeed.RemoveListener(ApplySlow);
        _Nav.UpdateSpeed();
    }

    private void ApplySlow()
    {
        _Nav.CurrentSpeed = Mathf.Min(_Nav.CurrentSpeed, _Nav.BaseSpeed * SlowPercent);
    }

    private IEnumerator Coroutine_Duration()
    {
        while (_DurationLeft > 0)
        {
            _DurationLeft -= Time.deltaTime;
            yield return null;
        }
        Handler.RemoveCondition(this);
    }
}
