using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : UnitComponent
{
    private Navigator Nav;
    private Transform TargetTransform;

    private void Start()
    {
        TargetTransform = CheckpointManager.Instance.GetNextCheckpoint( null );
        Nav = ThisUnit.MyNavigator;
        Nav.UE_ReachedDestination.AddListener(ReachedDestination);
        Nav.UE_PathComplete.AddListener(BeginMoving);
        Nav.SetTarget(TargetTransform);
    }

    private void ReachedDestination()
    {
        ThisUnit.Remove();
        //TAG:TODO : Call function in manager
    }

    private void BeginMoving()
    {
        //TAG:TODO: Make everything visible and enable all the relevant components
        Nav.CanMove = true;
    }
}
